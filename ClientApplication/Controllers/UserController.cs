using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ClientApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientApplication.Controllers
{
    public class UserController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(UserController));

        public IActionResult Login()
        {
            _log4net.Info("Log in page Displayed");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Email,Password")] LoginDetail info)
        {

            _log4net.Info("Login Data is going to send for authorization and token generation to AuthorizationService");

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(info), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44362/api/AuthToken", content))
                {

                    if (!response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Login");
                    }

                    var data = await response.Content.ReadAsStringAsync();

                    info = JsonConvert.DeserializeObject<LoginDetail>(data);

                    HttpContext.Session.SetString("token", info.Token);
                    HttpContext.Session.SetInt32("userId", info.Id);
                    HttpContext.Session.SetString("email", info.Email);
                    return RedirectToAction("Index", "Vehicle");
                }
            }
        }

        public IActionResult Register()
        {
            _log4net.Info("Register page displayed");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([Bind("Name,Email,Password,Gender,PhoneNo,Address")]User user)
        {

            _log4net.Info("Registeration data sent to AuthorizationService for registration");

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44362/api/User", content))
                {

                    if (response.IsSuccessStatusCode)
                    {

                        _log4net.Info("Registeration successfull");

                        return RedirectToAction("Login");
                    }
                    return View();
                }
            }
        }
        public IActionResult Logout()
        {

            _log4net.Info("Logout and clear all session details");
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
