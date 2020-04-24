using CarRent.Contexts.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarRepository _repo;

        public CarController(ICarRepository repo)
        {
            _repo = repo;
        }

        [HttpGet(Name = "Get"), Authorize]
        public JsonResult Get()
        {
            return new JsonResult(_repo.GetAll());
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
