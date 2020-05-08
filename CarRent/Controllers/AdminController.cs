using System;
using System.Collections.Generic;
using CarRent.Contexts.Interfaces;
using CarRent.Contexts.Models.Core;
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
        public IActionResult Add([FromBody] Car value)
        {
            Car found = _repo.Find(value.Id);
            if (found is null)
            {
                return Conflict("Car already exists!");
            }

            _repo.Add(value);

            _repo.Save();

            return Ok();
        }

        [HttpPost("update")]
        public IActionResult Update([FromBody] Car value)
        {
            Car found = _repo.Find(value.Id);
            if(found is null)
            {
                return ValidationProblem("Car not found!");
            }


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
