using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CarRent.Contexts.Interfaces;
using CarRent.Contexts.Models.Core;
using CarRent.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CarRent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public AuthController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("test messages");
        }

        [HttpGet("login"), AllowAnonymous]
        public IActionResult Authenticate([FromBody] UserLoginDto user)
        {
            _repo.Validate


            string now = DateTime.Now.ToString();

            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "token"),
                new Claim(JwtRegisteredClaimNames.Exp, now),
                new Claim(JwtRegisteredClaimNames.Nbf, now),
                new Claim(JwtRegisteredClaimNames.Iat, now),
                new Claim(JwtRegisteredClaimNames.Email, now),
                new Claim(JwtRegisteredClaimNames.Iat, now),
            };

            byte[] secretBytes = Encoding.UTF8.GetBytes("pepsjvxyjcvpsdjvélyxvéléá");
            var key = new SymmetricSecurityKey(secretBytes);

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                Request.Host.ToString(),
                Request.Host.ToString(),
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(8),
                signingCredentials);

            var jsonToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { token = jsonToken });
        }
    }
}