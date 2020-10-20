using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentService.Models;
using RentService.Repository;

namespace RentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentController : ControllerBase
    {
        private readonly ICartRepository<Cart> cartRepository;

        public RentController(ICartRepository<Cart> _cartRepository)
        {
            cartRepository = _cartRepository;
        }

        [HttpGet("{id}")]
        public async Task<List<int>> Get(int id)
        {
            return await cartRepository.GetAll(id); 
        }

        [HttpPost]
        public async Task<bool> Post([FromBody]Cart cart)
        {
            return await cartRepository.Add(cart);
        }

    }
}
