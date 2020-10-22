using AuthorizationService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VehicleService.Repository;

namespace AuthorizationService.Repository
{
    public class CartOperations : IAuthRepository<Cart>
    {
        readonly log4net.ILog _log4net;

        public CartOperations()
        {
            _log4net = log4net.LogManager.GetLogger(typeof(CartOperations));
        }

        public async Task<bool> Add(Cart cart)
        {
            try
            {
                _log4net.Info(nameof(CartOperations) + "invoked");


                using (var httpClient = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(cart), Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync("https://localhost:44316/api/Rent", content);

                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception e)
            {
                _log4net.Error("Error occured from " + nameof(CartOperations) + "Error Message " + e.Message);
                return false;
            }
        }

        public Task<Cart> Get(string a, string b)
        {
            throw new NotImplementedException();
        }

        public Task<List<Cart>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Cart>> Search(int id)
        {
            try
            {
                _log4net.Info(nameof(CartOperations) + "invoked");
                var carts = new List<Cart>();
                var query = "/" + id;
                HttpClient client = new HttpClient();
                var response = client.GetAsync("https://localhost:44316/api/Rent" + query).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    carts = JsonConvert.DeserializeObject<List<Cart>>(responseContent);
                }
                return carts;
            }
            catch (Exception e)
            {
                _log4net.Error("Error occured from " + nameof(CartOperations) + "Error Message " + e.Message);
                return null;
            }
        }
    }
}
