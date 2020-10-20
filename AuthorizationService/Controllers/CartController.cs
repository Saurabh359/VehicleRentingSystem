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
    public class CartController : ControllerBase
    {

        public readonly IAuthRepository<Cart> repository;

        public CartController(IAuthRepository<Cart> authRepository)
        {
            repository = authRepository;
        }

        /*
        [HttpGet("{id}")]
        public async Task<List<Cart>> Get(int id)
        {
           return await repository.Search(id);
        }

        */
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Cart cart)
        {
            return Ok(await repository.Add(cart));
        }
    }
}
