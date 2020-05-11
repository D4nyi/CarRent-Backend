using System;
using CarRent.Contexts.Interfaces;
using CarRent.Contexts.Models.Core;
using CarRent.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase, IDisposable
    {
        private readonly ICarRepository _repo;

        public AdminController(ICarRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] DetailDto value)
        {
            Car car = value;

            if (String.IsNullOrWhiteSpace(car.PremiseId))
            {
                return BadRequest(new ErrorModel
                {
                    Message = "Premise MUST be chosen!"
                });
            }

            try
            {
                if (_repo.CarExists(car))
                {
                    return Conflict("Car already exists!");
                }
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new ErrorModel
                {
                    Message = ex.Message,
                    CauseValue = nameof(value)
                });
            }

            _repo.Add(value);
            _repo.Save();

            return Ok();
        }

        [HttpPost("update")]
        public IActionResult Update([FromBody] DetailDto value)
        {
            Car car = value;

            if (String.IsNullOrWhiteSpace(car.Id))
            {
                return BadRequest(new ErrorModel
                {
                    Message = "Cannot update not existing car!"
                });
            }

            Car found = _repo.Find(value.Id);
            if (found is null)
            {
                return ValidationProblem("Car not found!");
            }

            found.Brand = car.Brand;
            found.Model = car.Model;
            found.Colour = car.Colour;
            found.LicensePlate = car.LicensePlate;
            found.EngineDescription = car.EngineDescription;
            found.Mileage = car.Mileage;
            
            if (!String.IsNullOrWhiteSpace(car.PremiseId))
            {
                found.PremiseId = car.PremiseId;
            }

            _repo.Save();

            return Ok();
        }

        [HttpPost("delete")]
        public IActionResult Delete([FromBody] DetailRequest value)
        {
            if (value is null || String.IsNullOrWhiteSpace(value.CarId))
            {
                return BadRequest(new ErrorModel
                {
                    Message = "Car Id is not set!"
                });
            }

            Car car = _repo.Find(value.CarId);
            if (car is null)
            {
                return NoContent();
            }

            _repo.Delete(car.Id);
            _repo.Save();

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
