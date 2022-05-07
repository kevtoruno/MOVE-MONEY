using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using MoveMoney.API.Models;
using MoveMoney.API.Helper;
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
        public async Task<IActionResult> GetCustomers([FromQuery]CustomerParams customerParams)
        {
            var customers = await _repo.GetCustomers(customerParams);
            
            Response.AddPagination(customers.CurrentPage, customers.PageSize, customers.TotalCount, customers.TotalPages);

            return Ok(customers);
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var getCustomers = await _repo.GetCustomer(id);

            return Ok(getCustomers);
        }

        //This creates a new customer
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