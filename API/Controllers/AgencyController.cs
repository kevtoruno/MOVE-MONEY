using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using MoveMoney.API.Data;
using System.Collections.Generic;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using MoveMoney.API.Helper;
using API.Controllers;

namespace MoveMoney.API.Controllers
{
    [Route("api/agencies")]
    public class AgencyController : BaseApiController
    {
        public async Task<IActionResult> GetAgency(int agencyId)
        {
            
            return Ok();
        }
    }
}