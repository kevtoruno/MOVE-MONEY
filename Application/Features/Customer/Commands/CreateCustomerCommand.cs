using Application.Core;
using Application.Core.Dtos;
using Application.Core.Interface;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customers.Commands;

public class CreateCustomerCommand : IRequest<Result<Unit>>
{
    public CustomerToCreateDto CustomerToCreateDto { get; set; }

    public CreateCustomerCommand(CustomerToCreateDto customerToCreateDto)
    {
        CustomerToCreateDto = customerToCreateDto;
    }
}

public class PlaceNewOrderHandler : IRequestHandler<CreateCustomerCommand, Result<Unit>>
{
    private readonly IMapper _mapper;
    private readonly IDataContext _context;
    public PlaceNewOrderHandler(IMapper mapper, IDataContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<Result<Unit>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerDto = request.CustomerToCreateDto;

        bool customerExists = await _context.Customers.AnyAsync(c => c.PhoneNumber == customerDto.PhoneNumber || 
            c.Identification == customerDto.Identification);

        if (customerExists == true)
            return Result<Unit>.Failure("This Phone Number or Identification has been used already.");     

        var customer = _mapper.Map<Customer>(customerDto); 

        customer.Identification = customer.Identification.ToLower();

        _context.Customers.Add(customer);

        if (await _context.SaveChangesAsync() > 0)
        {
            return Result<Unit>.CreatedAtRoute(Unit.Value, "GetCustomer");
        } 

        return Result<Unit>.Failure("Unknown error.");
    }
}
