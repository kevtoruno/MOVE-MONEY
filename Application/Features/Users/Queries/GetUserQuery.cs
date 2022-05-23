using Application.Core;
using Application.Core.Dtos;
using Application.Core.Interface;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries;

public class GetUserQuery : IRequest<Result<UserForDetailDto>>
{
    public int UserId { get; set; }
}

public class GetUserHandler : IRequestHandler<GetUserQuery, Result<UserForDetailDto>>
{
    private readonly IMoveMoneyRepository _repo;
    private readonly IMapper _mapper;
    public GetUserHandler(IMoveMoneyRepository repo, IMapper mapper)
    {
        _mapper = mapper;
        _repo = repo;
    }

    public async Task<Result<UserForDetailDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _repo.GetUser(request.UserId);

        var userForDetailDto = _mapper.Map<UserForDetailDto>(user);

        return Result<UserForDetailDto>.Success(userForDetailDto);
    }
}
