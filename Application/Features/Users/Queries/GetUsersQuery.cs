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

public class GetUsersQuery : IRequest<Result<IEnumerable<UserForDetailDto>>>
{
}

public class GetUsersHandler : IRequestHandler<GetUsersQuery, Result<IEnumerable<UserForDetailDto>>>
{
    private readonly IMoveMoneyRepository _repo;
    private readonly IMapper _mapper;
    public GetUsersHandler(IMoveMoneyRepository repo, IMapper mapper)
    {
        _mapper = mapper;
        _repo = repo;
    }

    public async Task<Result<IEnumerable<UserForDetailDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _repo.GetUsers();

        var usersToReturn = _mapper.Map<IEnumerable<UserForDetailDto>>(users);

        return Result<IEnumerable<UserForDetailDto>>.Success(usersToReturn);
    }
}
