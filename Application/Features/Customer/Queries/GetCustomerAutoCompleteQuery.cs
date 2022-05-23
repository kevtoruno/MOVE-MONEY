using Application.Core;
using Application.Core.Dtos;
using Application.Core.Interface;
using AutoMapper;
using MediatR;

namespace Application.Features.Customers.Queries;

public class GetCustomerAutoCompleteQuery : IRequest<Result<IEnumerable<CustomerToReturnDto>>>
{
    public string NameLike { get; set; }
}

public class GetCustomerAutoCompleteHandler : IRequestHandler<GetCustomerAutoCompleteQuery, Result<IEnumerable<CustomerToReturnDto>>>
{
    private IMoveMoneyRepository _repo;
    private IMapper _mapper;
    public GetCustomerAutoCompleteHandler(IMoveMoneyRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<CustomerToReturnDto>>> Handle(GetCustomerAutoCompleteQuery request, CancellationToken cancellationToken)
    {
        var customers = await _repo.GetCustomersAutoComplete(request.NameLike);

        var customerToReturn = _mapper.Map<IEnumerable<CustomerToReturnDto>>(customers);

        return Result<IEnumerable<CustomerToReturnDto>>.Success(customerToReturn);
    }
}

