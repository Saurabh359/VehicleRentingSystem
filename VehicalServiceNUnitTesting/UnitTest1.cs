using Moq;
using NUnit.Framework;
using VehicleService.Controllers;
using VehicleService.Models;
using VehicleService.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VehicalServiceNUnitTesting
{
    public class Tests
    {
        private Mock<IVehicleRepository<Vehicle>> repository;
        private VehicleController controller;

        List<Vehicle> vehicles = new List<Vehicle>()
        {
            new Vehicle{Id=1,Name="Monstar",Category="Bike",Color="Yellow",Available=true,RentPerHour=1100,Brand="Glace",UserId=2},
            new Vehicle{Id=2,Name="Wrath42",Category="Car",Color="Red",Available=true,RentPerHour=1300,Brand="Satrdom",UserId=3}
        };
        
        [SetUp]
        public void Setup()
        {
            repository = new Mock<IVehicleRepository<Vehicle>>();
            controller = new VehicleController(repository.Object);
        }

        [Test]
        public async Task Test_GetMethod_ReturnListOfVehicleAsync()
        {
            repository.Setup(r => r.FindAll()).ReturnsAsync(vehicles);

            var result =await controller.Get();

            Assert.AreEqual(result, vehicles);
        }

        [TestCase(3)]
        [TestCase(1)]
        public async Task Test_GetMethod_ReturnListOfId(int id)
        {
            repository.Setup(r => r.Search(id)).ReturnsAsync(vehicles);

            var result = await controller.Get(id);

            Assert.AreEqual(result, vehicles);
        }

        [Test]
        public async Task Test_PostMethod_ReturnTrueOnSuccessfullDataSubmisson()
        {
            repository.Setup(r => r.Add(vehicles[0])).ReturnsAsync(true);

            var result = await controller.Post(vehicles[0]);

            Assert.IsTrue(result);
        }
    }
}