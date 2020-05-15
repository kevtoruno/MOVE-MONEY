using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using MoveMoney.API.Models;
using MoveMoney.API.Data;
using System.Collections.Generic;

namespace MoveMoney.API.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMoveMoneyRepository _repo;
        public CustomersController(IMoveMoneyRepository repo)
        {
            this._repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var getCustomers = await _repo.GetCustomers();

            return Ok(getCustomers);
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var getCustomers = await _repo.GetCustomer(id);

            return Ok(getCustomers);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(Customer customer)
        {
            customer.Identification = customer.Identification.ToLower();

            if(await _repo.CustomerExists(customer.PhoneNumber, customer.Identification) != null)
            {
                return BadRequest("This Phone Number or Identification has been used already.");
            }

            var createdCustomer = await _repo.CreateCustomer(customer);

            return CreatedAtRoute("GetCustomer", routeValues: new { createdCustomer.Id}, value : createdCustomer);
        }
        
    }
}