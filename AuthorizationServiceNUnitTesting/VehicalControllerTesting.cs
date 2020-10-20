using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AuthorizationService.Controllers;
using AuthorizationService.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using VehicleService.Repository;

namespace AuthorizationServiceNUnitTesting
{
    [TestFixture]
    class VehicalControllerTesting
    {
        private Mock<IAuthRepository<Vehicle>> repository;
        private VehicleController controller;

        List<Vehicle> vehicles = new List<Vehicle>()
        {
            new Vehicle{Id=1,Name="Monstar",Category="Bike",Color="Yellow",Available=true,RentPerHour=1100,Brand="Glace",UserId=2},
            new Vehicle{Id=2,Name="Wrath42",Category="Car",Color="Red",Available=true,RentPerHour=1300,Brand="Satrdom",UserId=3}
        };

        [SetUp]
        public void Setup()
        {
            repository = new Mock<IAuthRepository<Vehicle>>();
            controller = new VehicleController(repository.Object);
        }

        [Test]
        public async Task Test_GetMethod_ReturnListOfVehicles()
        {
            repository.Setup(r => r.GetAll()).ReturnsAsync(vehicles);

            var result = await controller.Get();

            Assert.AreEqual(result, vehicles);
        }

        [TestCase(2)]
        public async Task Test_GetMethod_ReturnListOfVehiclesOnCart(int id)
        { 
            repository.Setup(r => r.Search(id)).ReturnsAsync(vehicles);

            var result = await controller.Get(id);

            Assert.AreEqual(result, vehicles);
        }

        [Test]
        public async Task Test_PostMethod_ReturnSuccessCode200fullDataSubmisson()
        {
            repository.Setup(r => r.Add(vehicles[0])).ReturnsAsync(true);

            var result = await controller.Post(vehicles[0]);

            var result2 = result as OkObjectResult;
            
            Assert.AreEqual(result2.StatusCode, 200);
        }
    }
}
