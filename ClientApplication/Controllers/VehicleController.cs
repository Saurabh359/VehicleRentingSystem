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
    public class VehicleController : Controller
    {

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(VehicleController));

        public async Task<IActionResult> Index()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                ViewData["Status"] = "Log In";

                _log4net.Info("Anonymous display");
            }
            else
            {
                _log4net.Info("Logged in User Display");

                ViewData["Status"] = "Log Out";
            }


            _log4net.Info("Vehicle Data Fetching from Vehicle Service Initiated");

            var vehicles = new List<Vehicle>();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44362/api/Vehicle");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer",HttpContext.Session.GetString("token"));
            HttpClient client = new HttpClient();
            var response = client.SendAsync(request).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(responseContent);
            }
            return View(vehicles);
        }

        public IActionResult Add()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                _log4net.Info("Anonymous Log in try to add vehicle but redirected to login page");

                return RedirectToAction("Login", "User");
            }
            _log4net.Info("User started adding new vehicle ");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([Bind("Name,Color,RentPerHour,Brand,Available,Category")] Vehicle vehicle)
        {

            _log4net.Info("Vehicle data going to pass to Vechicle service ");

            vehicle.UserId = (int)HttpContext.Session.GetInt32("userId");
            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(vehicle), Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44362/api/Vehicle")
                {
                    Content = content
                };

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                
                using( var response = await httpClient.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        _log4net.Info("Vehicle Data Saved Successfuly");

                        return RedirectToAction("Index");
                    }

                    _log4net.Info("Vehicle Data Not saved due to : "+response.StatusCode);
                }
            }

            return View();
        }
    }
}
