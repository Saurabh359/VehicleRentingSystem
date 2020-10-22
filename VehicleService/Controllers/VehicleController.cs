using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleService.Models;
using VehicleService.Repository;

namespace VehicleService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleRepository<Vehicle> repository;
        
        public VehicleController(IVehicleRepository<Vehicle> _repository )
        {
            repository = _repository;
        }
        // GET: api/<VehicleController>
        [HttpGet]
        public async Task<List<Vehicle>> Get()
        {
              return await repository.FindAll();
        }

        [HttpGet("{id}")]
        public async Task<Vehicle> Get(int id)
        {
            return await repository.Search(id);
        }

        // POST api/<VehicleController>
        [HttpPost]
        public async Task<bool> Post([FromBody] Vehicle vehicle)
        {
            return await repository.Add(vehicle);
        }

        /*
        // PUT api/<VehicleController>/5
        [HttpPut("{id}")]
        public async Task<bool> Put(int id, [FromBody] Vehicle vehicle)
        {
            var v = await context.Vehicles.FindAsync(id);
            if (v == null)
                return false;

            context.Vehicles.Update(vehicle);

            int a = await context.SaveChangesAsync();
            return a == 1;
        }

        // DELETE api/<VehicleController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            var vehicle = await context.Vehicles.FindAsync(id);

            if (vehicle == null)
                return false;

            context.Vehicles.Remove(vehicle);
            int a = await context.SaveChangesAsync();
            return a == 1;
        }
        */
    }
}
