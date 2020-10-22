using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AuthorizationService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VehicleService.Repository;

namespace AuthorizationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VehicleController : ControllerBase
    {
        public readonly IAuthRepository<Vehicle> repository;

        public VehicleController(IAuthRepository<Vehicle> authRepository)
        {
            repository = authRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<List<Vehicle>> Get()
        {
            return await repository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<List<Vehicle>> Get(int id)
        {
            return await repository.Search(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Vehicle vehicle)
        {
            return Ok(await repository.Add(vehicle));
        }
    }
}
