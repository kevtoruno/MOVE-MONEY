using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using MoveMoney.API.Models;
using MoveMoney.API.Helper;
using MoveMoney.API.Dtos;
using MoveMoney.API.Data;
using System.Collections.Generic;
using System.Security.Claims;
using AutoMapper;

namespace MoveMoney.API.Controllers
{
    [Route("api/orders")]
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

        /*[HttpPost]
        public async Task<IActionResult> CreateOrder(OrderForCreateDto orderForCreateDto)
        {
            if (orderForCreateDto.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var user = await _repo.GetUser(orderForCreateDto.UserId);

            if (orderForCreateDto.AgencyDestinationId == user.Agency.Id)
                return BadRequest("You cannot send money to the same Agency.");

            if (orderForCreateDto.RecipientId == orderForCreateDto.SenderId)
                return BadRequest("Sender and recipient are the same!");

            if (orderForCreateDto.Amount <= 0)
                return BadRequest("Amount must be higher than 0.");

            //

            var orderToReturn = _mapper.Map<Order>(orderForCreateDto);

            orderToReturn.Comission = orderToReturn.Amount * Convert.ToDecimal(comission);
            orderToReturn.Status = "Processing";
            orderToReturn.Taxes = orderToReturn.Amount * Convert.ToDecimal(0.10);
            orderToReturn.Total = orderToReturn.Amount + orderToReturn.Taxes + orderToReturn.Comission;
            _repo.Add(orderToReturn);

            if (await _repo.SaveAll())
            {

            }

        }*/

        [HttpGet("comission")]
        public async Task<IActionResult> GetComission(ComissionToGetDto comissionToGetDto)
        {
            var comission = await _repo.GetComissionValue(comissionToGetDto.Amount, comissionToGetDto.SenderId, comissionToGetDto.RecipientId);

            var comissionAmount = Convert.ToDecimal(comission) + comissionToGetDto.Amount;

            return Ok(comission);
        }
    }

}