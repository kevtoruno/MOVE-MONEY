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

        //Create order process
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrder(OrderForCreateDto orderForCreateDto)
        {
            //This checks if the user processing the order is logged or not.
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
        //This is to get the Comission % in specific, from the amount to transfer, senderCountryId, recipientCountryId
        [HttpGet("comission")]
        public async Task<IActionResult> GetComission([FromQuery] ComissionToGetDto comissionToGetDto)
        {
            var comission = await _repo.GetComissionValue(comissionToGetDto.Amount, comissionToGetDto.SenderId, comissionToGetDto.RecipientId);

            var comissionAmount = (comission * comissionToGetDto.Amount) + comissionToGetDto.Amount;

            return Ok(comissionAmount);
        }

        //This is to get the autocomplete results "like" for the customer's records
        [HttpGet("customer/autocomplete")]
        public async Task<IActionResult> GetCustomerAutoComplete([FromQuery] string names)
        {
            var customers = await _repo.GetCustomersAutoComplete(names);

            var customerToReturn = _mapper.Map<IEnumerable<CustomerToReturnDto>>(customers);

            return Ok(customerToReturn);
        }

        //This is to get the autocomplete results "like" for the customer's records
        [HttpGet("agency/autocomplete")]
        public async Task<IActionResult> GetAgencyAutoComplete([FromQuery] string name)
        {
            var agencies = await _repo.GetAgencyAutoComplete(name);

            var agencyToReturn = _mapper.Map<IEnumerable<AgencyToReturnDto>>(agencies);

            return Ok(agencyToReturn);
        }

        [HttpGet()]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _repo.GetOrders();

            var orderForList = _mapper.Map<IEnumerable<OrderForListDto>>(orders);

            return Ok(orderForList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _repo.GetOrder(id);

            var orderForDetail = _mapper.Map<OrderForDetailDto>(order);

            return Ok(orderForDetail);
        }

        // This Task is to change the status of the order from "Processing" to "Ready or "Processed" (depending if it is Pick up or Delivery), at the same time adding the money
        // to the User that processed this order
        [Authorize]
        [HttpPost("{userId}/{id}")]
        public async Task<IActionResult> ProcessOrder(int userId, int id)
        {
            //This checks if the user processing the order is logged or not.
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized("You are not authorized");
            }

            var order = await _repo.GetOrder(id);
            var user = await _repo.GetUser(userId);

            if (order.Status == "Processing")
            {
                if (order.DeliveryType == "Pick up")
                {
                    order.Status = "Ready";
                }
                else
                {
                    order.Status = "Processed";
                }
                
                user.Money += Convert.ToDecimal(order.Total);

                if (await _repo.SaveAll())
                {
                    return NoContent();
                }
            }
            return BadRequest("Order cannot be processed");
        }
    }

}