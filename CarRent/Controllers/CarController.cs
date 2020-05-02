using CarRent.Contexts.Interfaces;
using CarRent.Contexts.Models.Core;
using CarRent.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CarRent.Controllers
{
    [Route("api/[controller]")]
    [ApiController/*, Authorize*/]
    public sealed class CarController : ControllerBase, IDisposable
    {
        private readonly ICarRepository _repo;

        public CarController(ICarRepository repo)
        {
            _repo = repo;
        }

        [HttpGet, AllowAnonymous]
        public JsonResult Get()
        {
            return new JsonResult(_repo.GetAll());
        }

        [HttpPost("rent")]
        public IActionResult Rent([FromBody] RentingDto renting, [FromServices] IUserRepository userRepo)
        {
            if (renting is null)
            {
                return BadRequest(new ErrorModel
                {
                    CauseValue = "null",
                    CauseValueName = nameof(renting),
                    Message = "Renting parameter is not valid!"
                });
            }

            if (!userRepo.Validate(renting.Email, renting.Password))
            {
                return ValidationProblem("User credentials are incorrect!", "Email or password is incorrect!", 422, "Unprocessable Entity");
            }

            Renting rented = _repo.RentCar(renting.CarId, renting.UserId, renting.Start, renting.End);

            return new JsonResult(new
            {
                Rented = true,
                From = renting.Start,
                To = renting.End,
                Car = rented.Car.Brand + " " + rented.Car.Model
            });
        }

        [HttpPost("cancel")]
        public IActionResult Cancell([FromBody] RentingDto renting, [FromServices] IUserRepository userRepo)
        {
            if (renting is null)
            {
                return BadRequest(new ErrorModel
                {
                    CauseValue = "null",
                    CauseValueName = nameof(renting),
                    Message = "Renting parameter is not valid!"
                });
            }

            if (!userRepo.Validate(renting.Email, renting.Password))
            {
                return ValidationProblem("User credentials are incorrect!", "Email or password is incorrect!", 422, "Unprocessable Entity");
            }

            bool rentExists = _repo.RentingExists(renting.CarId, renting.UserId);

            if (rentExists && _repo.CancellRent(carId: renting.CarId, userId: renting.UserId))
            {
                return Ok("Renting cancelled!");
            }

            return BadRequest("Renting cannot be cancelled!");
        }

        [HttpPost("detail")]
        public IActionResult Detail([FromBody] DetailRequest detail)
        {
            if (detail is null || String.IsNullOrWhiteSpace(detail.CarId))
            {
                return BadRequest(new ErrorModel
                {
                    CauseValue = "whitespace",
                    CauseValueName = nameof(detail),
                    Message = "Invalid id!"
                });
            }

            Car car = _repo.Find(detail.CarId);
            if (car is null)
            {
                return NotFound("Car not found!");
            }

            return Ok(car);
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
