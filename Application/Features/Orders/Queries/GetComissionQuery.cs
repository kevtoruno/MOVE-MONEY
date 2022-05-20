using Application.Core;
using Application.Core.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Queries;

public class GetComissionQuery : IRequest<Result<double>>
{
    public int CountrySenderId { get; set; }
    public int CountryRecipientId { get; set; }
    public double Amount { get; set; }
}

public class GetComissionHandler : IRequestHandler<GetComissionQuery, Result<double>>
{
    private IMoveMoneyRepository _repo;
    public GetComissionHandler(IMoveMoneyRepository repo)
    {
        _repo = repo;
    }
    
    public async Task<Result<double>> Handle(GetComissionQuery request, CancellationToken cancellationToken)
    {
        var comission = await _repo.GetComissionPerSenderAndCountry(request.CountrySenderId, request.CountryRecipientId);

        var comissionResult = comission.CalcComissionPerValue(request.Amount);

        return Result<double>.Success(comissionResult);
    }
}

