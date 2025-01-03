using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Core.Dtos;
using Microsoft.AspNetCore.Authorization;
using MoveMoney.API.Helper;
using Application.Features.Orders.Queries;
using API.Controllers;
using Application.Features.Orders.Commands;
using Application.Features.Country.Queries;
using Application.Features.Agencies.Queries;

namespace MoveMoney.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : BaseApiController
    {
        //Create order process
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrder(OrderForCreateDto orderForCreateDto)
        {
            var result = await Mediator.Send(new PlaceNewOrderCommand(orderForCreateDto));

            return HandleResult(result);
        }
        
        [Authorize]
        [HttpPost("{userId}/{id}")]
        [ServiceFilter(typeof(LogUserActivity))]
        public async Task<IActionResult> ProcessOrder(int userId, int id)
        {
            var result = await Mediator.Send(new ProcessOrderCommand { UserId = userId, OrderId = id});

            return HandleResult(result);       
        }

        [HttpGet("comission")]
        public async Task<IActionResult> GetComission([FromQuery] GetComissionQuery query)
        {
            var comissionValue = await Mediator.Send(query);

            return HandleResult(comissionValue);
        }

        [HttpGet("country/{agencyId}")]
        public async Task<IActionResult> GetCountryByAgencyId(int agencyId)
        {
            var countryId = await Mediator.Send(new GetCountryByAgencyIdQuery { AgencyId = agencyId});

            return HandleResult(countryId);
        }

        [HttpGet("agency/autocomplete")]
        public async Task<IActionResult> GetAgencyAutoComplete([FromQuery] string name)
        {
            var agenciesDto = await Mediator.Send(new GetAgencyAutoCompleteQuery { NameLike = name });

            return HandleResult(agenciesDto);
        }

        [HttpGet()]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await Mediator.Send(new GetOrdersQuery());

            return HandleResult(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await Mediator.Send(new GetOrderQuery { OrderId = id});

            return HandleResult(order);
        }
    }

}