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

namespace Application.Features.Customers.Queries;

public class GetCustomerQuery : IRequest<Result<CustomerToDetailDto>>
{
    public int CustomerId { get; set; }
}

public class GetCustomerHandler : IRequestHandler<GetCustomerQuery, Result<CustomerToDetailDto>>
{
    private readonly IMoveMoneyRepository _repo;
    private readonly IMapper _mapper;
    public GetCustomerHandler(IMoveMoneyRepository repo, IMapper mapper)
    {
        _repo = repo;       
        var config = new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<Customer, CustomerToDetailDto>()
            .ForMember(dto => dto.TypeIdentification, opt => opt.MapFrom(c => c.TypeIdentification.Name));
        });

        _mapper = new Mapper(config);
    }

    public async Task<Result<CustomerToDetailDto>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        var customer = await _repo.GetCustomer(request.CustomerId);

        var customerToDetailDto = _mapper.Map<CustomerToDetailDto>(customer);

        return Result<CustomerToDetailDto>.Success(customerToDetailDto);
    }
}
