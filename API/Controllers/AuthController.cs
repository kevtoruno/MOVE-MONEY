using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Security.Claims;
using Application.Core.Dtos;
using Application.Features.Users.Commands;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : BaseApiController
{
    [HttpPost("register/{userId}")]
    public async Task<IActionResult> Register(int userId, UserForRegisterDto userForRegisterDto)
    {
        int userIdFromClaim = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        var result = await Mediator.Send(new RegisterUserCommand 
        { 
            UserIdFromClaim = userIdFromClaim, 
            LoggedInUserId = userId, 
            UserToRegisterDto = userForRegisterDto 
        });

        return HandleResult(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
    {
        var result = await Mediator.Send(new LoginUserCommand { UserForLoginDto = userForLoginDto });

        return HandleResult(result);
    }
}
