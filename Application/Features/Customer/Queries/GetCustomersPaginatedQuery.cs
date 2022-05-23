using Application.Core;
using Application.Core.Interface;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Queries;

public class GetCustomersPaginatedQuery : IRequest<Result<PagedList<Customer>>>
{
    public PaginationParams CustomerParams { get; set; }
}

public class GetCustomersHandler : IRequestHandler<GetCustomersPaginatedQuery, Result<PagedList<Customer>>>
{
    private IMoveMoneyRepository _repo;   
    public GetCustomersHandler(IMoveMoneyRepository repo)
    {
        _repo = repo;     
    }

    public async Task<Result<PagedList<Customer>>> Handle(GetCustomersPaginatedQuery request, CancellationToken cancellationToken)
    {
        var customerParams = request.CustomerParams;

        var customerQueryable = _repo.GetCustomers(customerParams);

        var paginatedCustomerList = await PagedList<Customer>.CreatePaginatedListAsync(customerQueryable, customerParams.PageNumber, customerParams.PageSize);

        return Result<PagedList<Customer>>.Success(paginatedCustomerList);
    }
}
