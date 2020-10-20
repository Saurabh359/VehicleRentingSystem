using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VehicleService.Models;

namespace VehicleService.Repository
{
    public class VehicleOperations : IVehicleRepository<Vehicle>
    {
        readonly log4net.ILog _log4net;

        private readonly VehicleDbContext context;
        
        public VehicleOperations(VehicleDbContext vehicleDbContext)
        {
            context = vehicleDbContext;
            _log4net = log4net.LogManager.GetLogger(typeof(VehicleOperations));
        }

        public async Task<bool> Add(Vehicle vehicle)
        {
            try
            {
                _log4net.Info(nameof(VehicleOperations) + "invoked");
                context.Vehicles.Add(vehicle);
                int a = await context.SaveChangesAsync();
                return a == 1;
            }
            catch(Exception e)
            {
                _log4net.Error("Error occured from "+ nameof(VehicleOperations) + "Error Message "+ e.Message);
                return false;
            }
        }

        public async Task<List<Vehicle>> FindAll()
        {
            try
            {
                _log4net.Info(nameof(VehicleOperations) + "invoked");
                var vlist = await context.Vehicles.ToListAsync();
                return vlist;
            }
            catch (Exception e)
            {
                _log4net.Error("Error occured from " + nameof(VehicleOperations) + "Error Message " + e.Message);
                return null;
            }
        }

        public async Task<List<Vehicle>> Search(int id)
        {
            try
            {
                _log4net.Info(nameof(VehicleOperations) + "invoked");

                var cart = new List<int>();
                var query = "/" + id;
                HttpClient client = new HttpClient();
                var response = await client.GetAsync("https://localhost:44316/api/Rent" + query);

                var responseContent = await response.Content.ReadAsStringAsync();
                cart = JsonConvert.DeserializeObject<List<int>>(responseContent);

                var vehicles = from i in context.Vehicles where cart.Any(x => x.Equals(i.Id)) select i;

                return await vehicles.ToListAsync();
            }
            catch (Exception e)
            {
                _log4net.Error("Error occured from " + nameof(VehicleOperations) + "Error Message " + e.Message);
                return null;
            }
        }
    }
}
