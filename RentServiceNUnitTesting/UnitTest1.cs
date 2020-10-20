using Moq;
using NUnit.Framework;
using RentService.Controllers;
using RentService.Models;
using RentService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentServiceNUnitTesting
{
    public class Tests
    {

        private Mock<ICartRepository<Cart>> repository;
        private RentController controller;

        Cart cart = new Cart { Id = 1, UserId = 2, VehicleId = 3, Booking = Convert.ToDateTime("2020-11-24 12:12:00 PM"), Hours = 3 };

        List<int> vehicleIds = new List<int> { 2, 3, 4, 5 };

        [SetUp]
        public void Setup()
        { 
            repository = new Mock<ICartRepository<Cart>>();
            controller = new RentController(repository.Object);
        }

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(1)]
        public async Task Test_GetMethod_ReturnListOfId(int id)
        {
            repository.Setup(r => r.GetAll(id)).ReturnsAsync(vehicleIds);

            var result = await controller.Get(id);

            Assert.AreEqual(result, vehicleIds);
        }

        [Test]
        public async Task Test_PostMethod_ReturnTrueOnSuccessfullDataSubmisson()
        {
            repository.Setup(r => r.Add(cart)).ReturnsAsync(true);

            var result = await controller.Post(cart);

            Assert.IsTrue(result);
        }
    }
}