using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using MoveMoney.API.Models;
using MoveMoney.API.Data;
using System.Collections.Generic;
using AutoMapper;
using MoveMoney.API.Dtos;

namespace MoveMoney.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMoveMoneyRepository _repo;
        private readonly IMapper _mapper;
        public UsersController(IMoveMoneyRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();

            var usersToReturn = _mapper.Map<IEnumerable<UserForDetailDto>>(users);

            return Ok(usersToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);

            var userToReturn = _mapper.Map<UserForDetailDto>(user);

            return Ok(userToReturn);
        }
    }
}