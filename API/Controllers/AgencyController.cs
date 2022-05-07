using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using MoveMoney.API.Models;
using MoveMoney.API.Dtos;
using MoveMoney.API.Data;
using System.Collections.Generic;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using MoveMoney.API.Helper;

namespace MoveMoney.API.Controllers
{
    [Route("api/agencies")]
    [ApiController]
    public class AgencyController : ControllerBase
    {
        private readonly IMoveMoneyRepository _repo;
        private readonly IMapper _mapper;
        public AgencyController(IMoveMoneyRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }

        /*[HttpGet("{id}")]
        public async Task<IActionResult> GetAgency(int id)
        {
            var comission = await _repo.GetComissionValue(comissionToGetDto.Amount, comissionToGetDto.SenderId, comissionToGetDto.RecipientId);

            var comissionAmount = (comission * comissionToGetDto.Amount) + comissionToGetDto.Amount;

            return Ok(comissionAmount);
        }*/
    }
}