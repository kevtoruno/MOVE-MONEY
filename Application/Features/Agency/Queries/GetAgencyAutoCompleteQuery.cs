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

namespace Application.Agencies.Queries;

public class GetAgencyAutoCompleteQuery : IRequest<Result<IEnumerable<AgencyToReturnDto>>>
{
    public string NameLike { get; set; }
}

public class GetOrdersHandler : IRequestHandler<GetAgencyAutoCompleteQuery, Result<IEnumerable<AgencyToReturnDto>>>
{
    private IMoveMoneyRepository _repo;
    private IMapper _mapper;
    public GetOrdersHandler(IMoveMoneyRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<AgencyToReturnDto>>> Handle(GetAgencyAutoCompleteQuery request, CancellationToken cancellationToken)
    {
        var agencies = await _repo.GetAgencyAutoComplete(request.NameLike);

        var agenciesToReturnDto = _mapper.Map<IEnumerable<AgencyToReturnDto>>(agencies);

        return Result<IEnumerable<AgencyToReturnDto>>.Success(agenciesToReturnDto);
    }
}

