using System.Threading.Tasks;
using Application.Core.Interface;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IDataContext _context;
        public AuthRepository(IDataContext context)
        {
            _context = context;

        }

        public async Task<User> Login(string userName, string password)
        {
            var user = await _context.Users
            .Include(u => u.UserRole)
            .Include(u => u.Agency)
            .FirstOrDefaultAsync(u => u.UserName == userName);

            if (user == null)
                return null;

            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

        public async Task<User> Register(User user, string password, CancellationToken cancellationToken)
        {
            byte[] passwordHash, passwordSalt;

            CreatePasswordHash(password, out passwordHash, out  passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync(cancellationToken);

            return user;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }    
        }
        
        public async Task<bool> UserExists(string userName)
        {
            if(await _context.Users.AnyAsync(u => u.UserName == userName))
                return true;
            else
                return false;
        }

        public async Task<bool> IsAdmin(int userId)
        {
            var user = await _context.Users
            .Include(u => u.UserRole)
            .FirstOrDefaultAsync(u => u.Id == userId);

            if(user.UserRole.RoleName != "Admin")
                return false;
            return true;
        }
    }
}