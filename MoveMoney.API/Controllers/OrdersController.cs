using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using MoveMoney.API.Models;
using MoveMoney.API.Helper;
using MoveMoney.API.Dtos;
using MoveMoney.API.Data;
using System.Diagnostics;
using System.Collections.Generic;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace MoveMoney.API.Controllers
{
    [Route("api/orders")]
    [Authorize]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMoveMoneyRepository _repo;
        private readonly IMapper _mapper;
        public OrdersController(IMoveMoneyRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderForCreateDto orderForCreateDto)
        {
            if (orderForCreateDto.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized("You are not loggedin");
            }           

            var user = await _repo.GetUser(orderForCreateDto.UserId);

            if (orderForCreateDto.AgencyDestinationId == user.Agency.Id)
                return BadRequest("You cannot send money to the same Agency.");

            if (orderForCreateDto.RecipientId == orderForCreateDto.SenderId)
                return BadRequest("Sender and recipient are the same!");

            if (orderForCreateDto.Amount <= 0)
                return BadRequest("Amount must be higher than 0.");

            var order = _mapper.Map<Order>(orderForCreateDto);

            order.Comission = orderForCreateDto.Comission - order.Amount;
            order.Status = "Processing";
            order.Taxes = order.Amount * 0.10;
            order.Total = order.Amount + order.Taxes + order.Comission;
            _repo.Add(order);

            var sender = await _repo.GetCustomer(order.SenderId);
            var recipient = await _repo.GetCustomer(order.RecipientId);
            var agency = await _repo.GetAgency(order.AgencyDestinationId);
            if (await _repo.SaveAll())
            {
                var orderForReturn = _mapper.Map<OrderForReturnDto>(order);

                orderForReturn.SenderName = sender.FirstName + " " + sender.LastName;
                orderForReturn.ReceiverName = recipient.FirstName + " " + recipient.LastName;
                orderForReturn.UserName = user.FirstName + " " + user.LastName;
                orderForReturn.AgencyDestinationName = agency.AgencyName;

                return CreatedAtRoute(null, orderForReturn);
            }
            return BadRequest("Transaction failed.");
        }

        [HttpGet("comission")]
        public async Task<IActionResult> GetComission(ComissionToGetDto comissionToGetDto)
        {
            var comission = await _repo.GetComissionValue(comissionToGetDto.Amount, comissionToGetDto.SenderId, comissionToGetDto.RecipientId);

            var comissionAmount = (comission * comissionToGetDto.Amount) + comissionToGetDto.Amount;

            return Ok(comissionAmount);
        }
    }

}