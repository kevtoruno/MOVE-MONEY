using Application.Core;
using Application.Core.Dtos;
using Application.Core.Interface;
using AutoMapper;
using MediatR;

namespace Application.Features.Users.Commands;

public class LoginUserCommand : IRequest<Result<UserLoggedInDto>>
{
    public UserForLoginDto UserForLoginDto { get; set; }
}

public class LoginUserHandler : IRequestHandler<LoginUserCommand, Result<UserLoggedInDto>>
{
    private readonly IAuthRepository _repo;
    private readonly IMapper _mapper;
    private readonly IJwtTokenCreator _tokenCreator;
    public LoginUserHandler(IAuthRepository repo, IMapper mapper, IJwtTokenCreator tokenCreator)
    {
        _repo = repo;
        _mapper = mapper;
        _tokenCreator = tokenCreator;
    }

    public async Task<Result<UserLoggedInDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var userForLoginDto = request.UserForLoginDto;
        var wrongPasswordResult = Result<UserLoggedInDto>.Unauthorized("Username or password are wrong.");

        var user = await _repo.GetUserByUserName(userForLoginDto.UserName);

        if (user == null)
            return wrongPasswordResult;

        bool isPasswordCorrect = user.ValidateIfPasswordIsCorrect(userForLoginDto.Password);

        if (isPasswordCorrect == false)
            return wrongPasswordResult;

        var userClaims = user.GetClaims();

        var userLoggedInDto = new UserLoggedInDto
        {
            JwtToken = _tokenCreator.GenerateJwtToken(userClaims),
            User = _mapper.Map<UserForDetailDto>(user)
        };

        return Result<UserLoggedInDto>.Success(userLoggedInDto);
    }
}
