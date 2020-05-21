using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using MoveMoney.API.Models;
using MoveMoney.API.Helper;
using MoveMoney.API.Data;
using System.Collections.Generic;

namespace MoveMoney.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly MoveMoneyRepository _repo;
        public OrdersController(MoveMoneyRepository repo)
        {
            _repo = repo;
        }

        /*[HttpPost]
        public async Task<IActionResult> CreateOrder()
        {

        }*/
    }

}