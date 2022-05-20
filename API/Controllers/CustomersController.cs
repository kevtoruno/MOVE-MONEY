using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using MoveMoney.API.Helper;
using MoveMoney.API.Data;
using System.Collections.Generic;
using MediatR;
using Application.Customers.Queries;
using Application.Core;
using Application.Core.Dtos;
using Application.Customers.Commands;

namespace API.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetCustomers([FromQuery]PaginationParams customerParams)
        {
            var paginatedCustomers = await Mediator.Send(new GetCustomersPaginatedQuery { CustomerParams = customerParams});
            
            Response.AddPagination(paginatedCustomers.Value.CurrentPage, paginatedCustomers.Value.PageSize, paginatedCustomers.Value.TotalCount, paginatedCustomers.Value.TotalPages);

            return HandleResult(paginatedCustomers);
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await Mediator.Send(new GetCustomerQuery { CustomerId = id });

            return HandleResult(customer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerToCreateDto customer)
        {
            var result = await Mediator.Send(new CreateCustomerCommand (customer));

            return HandleResult(result);
        }
        
        [HttpGet("customer/autocomplete")]
        public async Task<IActionResult> GetCustomerAutoComplete([FromQuery] string names)
        {
            var customersToReturn = await Mediator.Send(new GetCustomerAutoCompleteQuery { NameLike = names });

            return Ok(customersToReturn);
        }
    }
}