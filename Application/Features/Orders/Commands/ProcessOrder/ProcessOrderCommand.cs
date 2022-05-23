using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core;
using Application.Core.Dtos;
using Application.Core.Interface;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Orders.Commands;

public class ProcessOrderCommand :IRequest<Result<Unit>>
{
    public int UserId { get; set; }

    public int OrderId { get; set; }
}

public class ProcessOrderHandler: IRequestHandler<ProcessOrderCommand, Result<Unit>>
{
    private readonly IMoveMoneyRepository _repo;
    private readonly IMapper _mapper;
    private readonly IDataContext _context;

    public ProcessOrderHandler(IMoveMoneyRepository repo, IMapper mapper, IDataContext context)
    {
        _repo = repo;
        _mapper = mapper;
        _context = context;
    }

    public async Task<Result<Unit>> Handle(ProcessOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await _repo.GetOrder(command.OrderId);
        var user = await _repo.GetUser(command.UserId);

        bool orderProcessed = order.ProcessOrder();

        if (orderProcessed)
        {
            user.AddMoney(order.Total);
            return Result<Unit>.NoContent();
        }

        return Result<Unit>.Failure("Order cannot be processed");
    }
}