using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Core.Interface
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password, CancellationToken cancellationToken);
         Task<User> Login(string userName, string password);
         Task<bool> UserExists(string userName);
         Task<bool> IsAdmin(int userId);
    }
}