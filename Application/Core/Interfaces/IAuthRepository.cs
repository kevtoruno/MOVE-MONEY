using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Core.Interface
{
    public interface IAuthRepository
    {
         Task<User?> GetUserByUserName(string userName);
         Task<bool> UserExists(string userName);
         Task<bool> IsAdmin(int userId);
    }
}