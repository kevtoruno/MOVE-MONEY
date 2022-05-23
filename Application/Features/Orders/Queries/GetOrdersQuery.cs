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

namespace Application.Features.Orders.Queries;

public class GetOrdersQuery : IRequest<Result<IEnumerable<OrderForListDto>>>
{
}

public class GetOrdersHandler : IRequestHandler<GetOrdersQuery, Result<IEnumerable<OrderForListDto>>>
{
    private IMoveMoneyRepository _repo;
    private IMapper _mapper;
    public GetOrdersHandler(IMoveMoneyRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<OrderForListDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _repo.GetOrders();

        var orderForList = _mapper.Map<IEnumerable<OrderForListDto>>(orders);

        return Result<IEnumerable<OrderForListDto>>.Success(orderForList);
    }
}

