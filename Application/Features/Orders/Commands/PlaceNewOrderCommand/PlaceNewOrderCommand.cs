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

namespace Application.Orders.Commands;

public class PlaceNewOrderCommand : IRequest<Result<OrderForReturnDto>>
{
    public OrderForCreateDto OrderToCreateDto { get; set; }

    public PlaceNewOrderCommand(OrderForCreateDto order)
    {
        OrderToCreateDto = order;
    }
}

public class PlaceNewOrderHandler : IRequestHandler<PlaceNewOrderCommand, Result<OrderForReturnDto>>
{
    private readonly IMoveMoneyRepository _repo;
    private readonly IMapper _mapper;
    private readonly IDataContext _context;
    public PlaceNewOrderHandler(IMoveMoneyRepository repo, IMapper mapper, IDataContext context)
    {
        _repo = repo;
        _mapper = mapper;
        _context = context;
    }

    public async Task<Result<OrderForReturnDto>> Handle(PlaceNewOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToCreateDto = request.OrderToCreateDto;

        var user = await _repo.GetUser(orderToCreateDto.UserId);

        if (orderToCreateDto.AgencyDestinationId == user.Agency.Id)
            return Result<OrderForReturnDto>.Failure("You cannot send money to the same Agency.");

        var orderToCreate = _mapper.Map<Order>(orderToCreateDto);

        orderToCreate.InitializeProcessingOrder(orderToCreateDto.Comission);
        _context.Orders.Add(orderToCreate);
        await _context.SaveChangesAsync();

        var sender = await _repo.GetCustomer(orderToCreate.SenderId);
        var recipient = await _repo.GetCustomer(orderToCreate.RecipientId);
        var agency = await _repo.GetAgency(orderToCreate.AgencyDestinationId);
        
        var orderForReturn = _mapper.Map<OrderForReturnDto>(orderToCreate);

        orderForReturn.SenderName = sender.FirstName + " " + sender.LastName;
        orderForReturn.ReceiverName = recipient.FirstName + " " + recipient.LastName;
        orderForReturn.UserName = user.FirstName + " " + user.LastName;
        orderForReturn.AgencyDestinationName = agency.AgencyName;

        return Result<OrderForReturnDto>.CreatedAtRoute(orderForReturn);
    }
}

