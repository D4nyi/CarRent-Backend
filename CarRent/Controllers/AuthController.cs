using CarRent.Contexts.Interfaces;
using CarRent.Contexts.Models.Core;
using CarRent.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarRent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class AuthController : ControllerBase, IDisposable
    {
        private readonly IUserRepository _repo;

        public AuthController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("login"), AllowAnonymous]
        public IActionResult Authenticate(LoginDto login)
        {
            if (login is null)
            {
                return BadRequest(new ErrorModel
                {
                    CauseValue = "null",
                    CauseValueName = nameof(login),
                    Message = "Login parameter is not valid!"
                });
            }

            if (!_repo.Validate(login.Email, login.Password))
            {
                return ValidationProblem("User credentials are incorrect!", "Email or password is incorrect!", 422, "Unprocessable Entity");
            }

            User user = _repo.FindByEmail(login.Email);

            string now = DateTime.Now.ToString();
            DateTime expires = DateTime.Now.AddHours(1);

            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "token"),
                new Claim(JwtRegisteredClaimNames.Exp, expires.ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, now),
                new Claim(JwtRegisteredClaimNames.Iat, now),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
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

            return Ok(new { email = user.Email, name = user.UserName, token = jsonToken, expirationDate = expires });
        }

        [HttpPost("register"), AllowAnonymous]
        public IActionResult RegisterUser([FromBody] RegisterDto register)
        {
            if (register is null)
            {
                return BadRequest(new ErrorModel
                {
                    CauseValue = "null",
                    CauseValueName = nameof(register),
                    Message = "Register parameter is not valid!"
                });
            }

            if (_repo.EmailExists(register.Email))
            {
                return BadRequest(new ErrorModel
                {
                    CauseValue = "Email",
                    CauseValueName = nameof(register.Email),
                    Message = "Email already exists!"
                });
            }

            if (String.IsNullOrWhiteSpace(register.UserName))
            {
                register.UserName = register.FullName.ToLower().Replace(' ', '_');
            }

            if (_repo.UserNameExists(register.UserName))
            {
                register.UserName += register.BirthDate.Year.ToString().Substring(2, 2);
            }

            User user = _repo.Register(new User
            {
                Email = register.Email,
                Address = register.Address,
                FirstName = register.FirstName,
                LastName = register.LastName,
                UserName = register.UserName,
                BirthDate = register.BirthDate,
            }, register.Password);

            _repo.Save();

            return new JsonResult(new RegisterDto
            {
                Email = user.Email,
                Address = user.Address,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                BirthDate = user.BirthDate,
            });
        }

        [HttpPost("test"), AllowAnonymous]
        public IActionResult Test([FromBody] RegisterDto register)
        {
            return Ok();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _repo.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}