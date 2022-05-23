using Application.Core;
using Application.Core.Dtos;
using Application.Core.Interface;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands;
public class RegisterUserCommand : IRequest<Result<UserForDetailDto>>
{
    public int UserIdFromClaim { get; set; }
    public int LoggedInUserId { get; set; }
    public UserForRegisterDto UserToRegisterDto { get; set; }
}

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Result<UserForDetailDto>>
{
    private readonly IAuthRepository _repo;
    private readonly IMapper _mapper;
    private readonly IDataContext _context;
    public RegisterUserHandler(IAuthRepository repo, IMapper mapper, IDataContext context)
    {
        _repo = repo;
        _mapper = mapper;
        _context = context;
    }

    public async Task<Result<UserForDetailDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        Result<UserForDetailDto> result;

        var loggedInUserId = request.LoggedInUserId;
        var userIdFromClaim = request.UserIdFromClaim;
        var userForRegisterDto = request.UserToRegisterDto;
        userForRegisterDto.UserName = userForRegisterDto.UserName.ToLower();

        if (await _repo.IsAdmin(request.LoggedInUserId) == false)
            result = Result<UserForDetailDto>.Unauthorized("Only admins can create new Users");
        else if(loggedInUserId != userIdFromClaim)
            result = Result<UserForDetailDto>.Unauthorized("Unauthorized");
        else if(await _repo.UserExists(userForRegisterDto.UserName))
            result = Result<UserForDetailDto>.Failure("Username already exists");
        else
        {
            var userToCreate = _mapper.Map<User>(userForRegisterDto);

            userToCreate.SetPassword(userForRegisterDto.Password);

            result = await PersistUser(userToCreate);
        }

        return result;
    }

    private async Task<Result<UserForDetailDto>> PersistUser(User userToPersist)
    {
        Result<UserForDetailDto> resultToReturn;

        await _context.Users.AddAsync(userToPersist);

        if (await _context.SaveChangesAsync() > 0)
        {
            var userToReturn = _mapper.Map<UserForDetailDto>(userToPersist);

            resultToReturn = Result<UserForDetailDto>.CreatedAtRoute(userToReturn);
        }
        else
        {
            resultToReturn = Result<UserForDetailDto>.Failure("Error");
        }

        return resultToReturn;
    }
}
