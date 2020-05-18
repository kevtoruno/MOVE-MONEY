using Microsoft.AspNetCore.Mvc;
using MoveMoney.API.Data;
using System.Threading.Tasks;
using MoveMoney.API.Models;
using MoveMoney.API;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Diagnostics;
using AutoMapper;
using MoveMoney.API.Dtos;

namespace MoveMoney.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public AuthController(IAuthRepository repo, IConfiguration config, IMapper mapper)
        {
            _mapper = mapper;
            _config = config;
            _repo = repo;
        }

        [HttpPost("register/{userId}")]
        public async Task<IActionResult> Register(int userId, UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.UserName = userForRegisterDto.UserName.ToLower();
            if(!await _repo.IsAdmin(userId))
                return Unauthorized("Only admins can create new Users");

            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            if(await _repo.UserExists(userForRegisterDto.UserName))
                return BadRequest("Username already exists");

            var userToCreate = _mapper.Map<User>(userForRegisterDto);

            var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);

            var userToReturn = _mapper.Map<UserForDetailDto>(createdUser);

            return CreatedAtRoute(null, userToReturn);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await _repo.Login(userForLoginDto.UserName.ToLower(), userForLoginDto.Password);

            if(userFromRepo == null)
                return Unauthorized("Username or password are wrong.");
            
            var claims = new []
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.UserName),
                new Claim(ClaimTypes.Role, userFromRepo.UserRole.RoleName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var user = _mapper.Map<UserForDetailDto>(userFromRepo);

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok( new 
            {
                token = tokenHandler.WriteToken(token),
                user
            });
        }
    }
}