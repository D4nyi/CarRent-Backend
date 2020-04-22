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
        public IActionResult Authenticate()
        {
            //if (userDto is null)
            //{
            //    return BadRequest(new ErrorModel
            //    {
            //        CauseValue = "null",
            //        CauseValueName = nameof(userDto),
            //        Message = "Login parameter is not valid!"
            //    });
            //}

            //if (!_repo.Validate(userDto.Email, userDto.Password))
            //{
            //    return ValidationProblem("User credentials are incorrect!", "Email or password is incorrect!", 422, "Unprocessable Entity");
            //}

            //User user = _repo.FindByEmail(userDto.Email);

            string now = DateTime.Now.ToString();
            DateTime expires = DateTime.Now.AddHours(1);

            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "token"),
                new Claim(JwtRegisteredClaimNames.Exp, expires.ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, now),
                new Claim(JwtRegisteredClaimNames.Iat, now),
                new Claim(JwtRegisteredClaimNames.Email, "email@example.com"),
                new Claim("nickname", "UserName")
            };

            byte[] secretBytes = Encoding.UTF8.GetBytes("pepsjvxyjcvpsdjvélyxvéléá");
            var key = new SymmetricSecurityKey(secretBytes);

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                Request.Host.ToString(),
                Request.Host.ToString(),
                claims,
                notBefore: DateTime.Now,
                expires: expires,
                signingCredentials);

            string jsonToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { token = jsonToken, expires});
        }
    }
}