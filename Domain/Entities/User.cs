using System.Security.Claims;

namespace Domain.Entities
{
    public class User 
    {
        public int Id { get; set; }
        public Agency Agency { get; set; }
        public int AgencyId { get; set; }
        public UserRole UserRole { get; set; }
        public int UserRoleId { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Created { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public decimal Money { get; set; }

        public void AddMoney(double amount)
        {
            Money += Convert.ToDecimal(amount);
        }

        public void SetPassword(string password)
        {
            byte[] passwordHash, passwordSalt;

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            PasswordSalt = passwordSalt;
            PasswordHash = passwordHash;         
        }

        public bool ValidateIfPasswordIsCorrect(string password)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != PasswordHash[i]) return false;
                }
            }

            return true;
        }

        public Claim[] GetClaims()
        {
            var claims = new []
            {
                new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                new Claim(ClaimTypes.Name, UserName),
                new Claim(ClaimTypes.Role, UserRole.RoleName)
            };

            return claims;
        }
    }
}