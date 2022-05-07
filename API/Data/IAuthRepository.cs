using System.Threading.Tasks;
using MoveMoney.API.Models;

namespace MoveMoney.API.Data
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string userName, string password);
         Task<bool> UserExists(string userName);
         Task<bool> IsAdmin(int userId);
    }
}