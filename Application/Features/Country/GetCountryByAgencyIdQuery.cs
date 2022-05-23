using Application.Core;
using Application.Core.Interface;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Country.Queries;
public class GetCountryByAgencyIdQuery: IRequest<Result<int>>
{
    public int AgencyId { get; set; }
}

public class GetCountryByAgencyIdHandler : IRequestHandler<GetCountryByAgencyIdQuery, Result<int>>
{
    private IMoveMoneyRepository _repo;
    private IMapper _mapper;
    public GetCountryByAgencyIdHandler(IMoveMoneyRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(GetCountryByAgencyIdQuery request, CancellationToken cancellationToken)
    {
        var countryId = await _repo.GetCountryIdByAgency(request.AgencyId);

        if(countryId > 0)
            return Result<int>.Success(countryId);

        return Result<int>.Failure("Country id not found");
    }
}

