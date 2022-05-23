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

public class GetOrderQuery : IRequest<Result<OrderForDetailDto>>
{
    public int OrderId { get; set; }
}

public class GetOrderHandler : IRequestHandler<GetOrderQuery, Result<OrderForDetailDto>>
{
    private IMoveMoneyRepository _repo;
    private IMapper _mapper;
    public GetOrderHandler(IMoveMoneyRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<Result<OrderForDetailDto>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await _repo.GetOrder(request.OrderId);

        var orderForDetail = _mapper.Map<OrderForDetailDto>(order);

        return Result<OrderForDetailDto>.Success(orderForDetail);
    }
}

