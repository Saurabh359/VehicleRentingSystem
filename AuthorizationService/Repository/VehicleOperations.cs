using AuthorizationService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VehicleService.Repository
{
    public class VehicleOperations : IAuthRepository<Vehicle>
    {
        readonly log4net.ILog _log4net;

        public VehicleOperations()
        {
            _log4net = log4net.LogManager.GetLogger(typeof(VehicleOperations));
        }

        public async Task<bool> Add(Vehicle vehicle)
        {
            try
            {
                _log4net.Info(nameof(VehicleOperations) + "invoked");

                using (var httpClient = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(vehicle), Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync("https://localhost:44385/api/Vehicle", content);

                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception e)
            {
                _log4net.Error("Error occured from " + nameof(VehicleOperations) + "Error Message " + e.Message);
                return false;
            }
        }

        public Task<Vehicle> Get(string a, string b)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Vehicle>> GetAll()
        {
            try
            {
                _log4net.Info(nameof(VehicleOperations) + "invoked");

                var vehicles = new List<Vehicle>();
                HttpClient client = new HttpClient();
                var response = client.GetAsync("https://localhost:44385/api/Vehicle").Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(responseContent);
                }

                return vehicles;
            }
            catch (Exception e)
            {
                _log4net.Error("Error occured from " + nameof(VehicleOperations) + "Error Message " + e.Message);
                return null;
            }
        }

        public Task<List<Vehicle>> Search(int id)
        {
            throw new NotImplementedException();
            /*
            try
            {
                _log4net.Info(nameof(VehicleOperations) + "invoked");

                var vehicles = new List<Vehicle>();
                var query = "/" + id;
                HttpClient client = new HttpClient();
                var response = client.GetAsync("https://localhost:44385/api/Vehicle" + query).Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(responseContent);
                }

                return vehicles;
            }
            catch (Exception e)
            {
                _log4net.Error("Error occured from " + nameof(VehicleOperations) + "Error Message " + e.Message);
                return null;
            }
            */
        }
    }
}
