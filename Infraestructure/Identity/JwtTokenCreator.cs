using Application.Core.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Identity;

public class JwtTokenCreator : IJwtTokenCreator
{
    private readonly IConfiguration _config;
    public JwtTokenCreator(IConfiguration configuration)
    {
        _config = configuration;
    }

    public string GenerateJwtToken(Claim[] claims)
    {
        string jwtToken;

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

        var token = tokenHandler.CreateToken(tokenDescriptor);
        jwtToken = tokenHandler.WriteToken(token);

        return jwtToken;
    }
}
