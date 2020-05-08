using System;
using CarRent.Contexts.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize(Roles = "Admin")]
    public class PremiseController : ControllerBase, IDisposable
    {
        private readonly IPremiseRepository _repo;

        public PremiseController(IPremiseRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult(_repo.GetAll());
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
