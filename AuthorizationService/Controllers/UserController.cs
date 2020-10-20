using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationService.Data;
using AuthorizationService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleService.Repository;

namespace AuthorizationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IAuthRepository<User> repository;

        public UserController(IAuthRepository<User> authRepository)
        {
            repository = authRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            return Ok(await repository.Add(user));
        }
    }
}
