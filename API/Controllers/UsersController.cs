using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Core.Dtos;
using Application.Features.Users.Queries;

namespace API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await Mediator.Send(new GetUsersQuery ());

            return HandleResult(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await Mediator.Send(new GetUserQuery { UserId = id });

            return HandleResult(user);
        }
    }
}