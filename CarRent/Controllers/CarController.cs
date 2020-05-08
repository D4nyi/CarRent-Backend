using CarRent.Contexts.Interfaces;
using CarRent.Contexts.Models.Core;
using CarRent.Dtos;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;

namespace CarRent.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize(Roles = "User")]
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
            List<Car> cars = _repo.GetAll(true);

            return new JsonResult(cars.ConvertAll(car => new DetailDto
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Colour = car.Colour,
                LicensePlate = car.LicensePlate,
                EngineDescription = car.EngineDescription,
                Mileage = car.Mileage,
                PremiseName = car.Premise.Name + ", " + car.Premise.Address,
                Rented = car.RentingId != null
            }));
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

            string userId = userRepo.GetUserId(renting.Email, renting.Password);

            Renting rented = _repo.RentCar(renting.CarId, userId, renting.Start, renting.End);

            _repo.Save();

            return new JsonResult(new
            {
                Rented = true,
                From = renting.Start,
                To = renting.End,
                Car = rented.Car.Brand + " " + rented.Car.Model
            });
        }

        [HttpGet("rented/{email}")]
        public IActionResult Rented(string email, [FromServices] IUserRepository userRepo)
        {
            if (String.IsNullOrWhiteSpace(email))
            {
                return BadRequest(new ErrorModel
                {
                    CauseValueName = nameof(email),
                    Message = "Email is not valid!"
                });
            }

            User user = userRepo.FindByEmail(email);

            if (user.RentingId is null)
            {
                return NoContent();
            }

            Car car = _repo.GetCarByRentingId(user.RentingId);

            return Ok(new DetailDto
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Colour = car.Colour,
                LicensePlate = car.LicensePlate,
                EngineDescription = car.EngineDescription,
                Mileage = car.Mileage,
                PremiseName = car.Premise.Name + ", " + car.Premise.Address,
                Rented = car.RentingId != null
            });
        }

        [HttpPost("cancel")]
        public IActionResult Cancel([FromBody] RentingDto renting, [FromServices] IUserRepository userRepo)
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

            string userId = userRepo.GetUserId(renting.Email, renting.Password);

            bool rentExists = _repo.RentingExists(renting.CarId, userId);

            if (rentExists && _repo.CancellRent(userId: userId))
            {
                _repo.Save();
                return Ok("Renting cancelled!");
            }

            return NoContent();
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

            Car car = _repo.FindAndLoadPremise(detail.CarId);
            if (car is null)
            {
                return NotFound("Car not found!");
            }

            return Ok(new DetailDto
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Colour = car.Colour,
                LicensePlate = car.LicensePlate,
                EngineDescription = car.EngineDescription,
                Mileage = car.Mileage,
                PremiseName = car.Premise.Name + ", " + car.Premise.Address,
                Rented = car.RentingId != null
            });
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
