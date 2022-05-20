using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Application.Core.Dtos;
using MoveMoney.API.Data;
using System.Collections.Generic;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using MoveMoney.API.Helper;
using Application.Orders.Queries;
using API.Controllers;
using Application.Orders.Commands;
using Application.Customers.Queries;
using Application.Country.Queries;
using Application.Agencies.Queries;

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

            return Ok(agenciesDto);
        }

        [HttpGet()]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await Mediator.Send(new GetOrdersQuery());

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await Mediator.Send(new GetOrderQuery { OrderId = id});

            return HandleResult(order);
        }

        // This Task is to change the status of the order from "Processing" to "Ready or "Processed" (depending if it is Pick up or Delivery), at the same time adding the money
        // to the User that processed this order
        [Authorize]
        [HttpPost("{userId}/{id}")]
        [ServiceFilter(typeof(LogUserActivity))]
        public async Task<IActionResult> ProcessOrder(int userId, int id)
        {
            var result = await Mediator.Send(new ProcessOrderCommand { UserId = userId, OrderId = id});

            return HandleResult(result);       
        }
    }

}