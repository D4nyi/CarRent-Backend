using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CarRent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("test messages");
        }
        
        [HttpGet("get"), AllowAnonymous]
        public IActionResult Authenticate()
        {
            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "some_id"),
                new Claim("granny", "cookie")
            };

            byte[] secretBytes = Encoding.UTF8.GetBytes("pepsjvxyjcvpsdjvélyxvéléá");
            var key = new SymmetricSecurityKey(secretBytes);

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                "https://localhost:5001/",
                "https://localhost:5001/",
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(8),
                signingCredentials);

            var jsonToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { token = jsonToken });
        }
    }
}