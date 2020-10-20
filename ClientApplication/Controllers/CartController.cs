using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ClientApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientApplication.Controllers
{
    public class CartController : Controller
    {
        public async Task<IActionResult> Index()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                return RedirectToAction("Login", "User");
            }

            HttpClient client = new HttpClient();

            var query = "/" + HttpContext.Session.GetInt32("userId");

            var items = new List<Vehicle>();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44362/api/Vehicle" + query);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            var response = client.SendAsync(request).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                items = JsonConvert.DeserializeObject<List<Vehicle>>(responseContent);
            }

            return View(items);

        }

        public IActionResult Add(int id)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                return RedirectToAction("Login", "User");
            }

            ViewData["vehicleId"] = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([Bind("VehicleId,Booking,Hours")] Cart cart)
        {
            cart.UserId = (int)HttpContext.Session.GetInt32("userId");

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(cart), Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44362/api/Cart")
                {
                    Content = content
                };

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                using (var response = await httpClient.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index","Vehicle");
                    }
                }
            }
            return View();
        }
    }
}
