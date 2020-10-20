using AuthorizationService.Controllers;
using AuthorizationService.Models;
using AuthorizationService.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleService.Repository;

namespace AuthorizationServiceNUnitTesting
{
    [TestFixture]
    class AuthTokenControllerTesting
    {
        private AuthTokenController controller;
        private Mock<IConfiguration> config;
        private Mock<IAuthRepository<User>> repository;

        [SetUp]
        public void Setup()
        {
            config = new Mock<IConfiguration>();
            repository = new Mock<IAuthRepository<User>>();
            controller = new AuthTokenController(config.Object,repository.Object);
        }

        User user = new User
        {
            Id = 2,
            Name = "Saurabh",
            Gender = "Male",
            Password = "hello",
            Email = "s123@gmail.com",
            PhoneNo = "9865347123",
            Address = "Haldwani"
        };

        UserData userData = new UserData
        {
            Id = 0,
            Name = "",
            Email = "s123@gmail.com",
            Password = "hello",
            Token = ""
        };

        [Test]
        public async Task Test_PostMethod_ReturnUserDataWithSuccessResponse()
        {
            config.Setup(p => p["Jwt:Key"]).Returns("ssdfddsfffgsdfsfdfgg");
            config.Setup(p => p["Jwt:Issuer"]).Returns("InventoryAuthenticationServer");
            config.Setup(p => p["Jwt:Audience"]).Returns("InventoryServicePostmanClient");

            var mock = new Mock<IAuthRepository<User>>();
            mock.Setup(p => p.Get(userData.Email,userData.Password)).ReturnsAsync(user);

            var result = await controller.Post(userData);
            var result2 = result as OkObjectResult;
            Assert.AreEqual(200,result2.StatusCode);
        }

        [Test]
        public async Task Test_PostMethod_ReturnBadRequest()
        {
            UserData userDatatemp=null;
            var result = await controller.Post(userDatatemp);
            var result2 = result as OkObjectResult;
            Assert.AreEqual(400, result2.StatusCode);
        }
    }
}
