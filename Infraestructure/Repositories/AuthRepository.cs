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

        public async Task<User?> GetUserByUserName(string userName)
        {
            var user = await _context.Users
            .Include(u => u.UserRole)
            .Include(u => u.Agency)
            .FirstOrDefaultAsync(u => u.UserName == userName);

            return user;
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
            bool isAdmin = true;

            var user = await _context.Users
            .Include(u => u.UserRole)
            .FirstOrDefaultAsync(u => u.Id == userId);

            if(user == null || user.UserRole.RoleName != "Admin")
                isAdmin = false;

            return isAdmin;
        }
    }
}