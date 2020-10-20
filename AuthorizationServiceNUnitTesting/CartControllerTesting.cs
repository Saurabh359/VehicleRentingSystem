using AuthorizationService.Controllers;
using AuthorizationService.Models;
using Microsoft.AspNetCore.Mvc;
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
    class CartControllerTesting
    {
        private Mock<IAuthRepository<Cart>> repository;
        private CartController controller;

        Cart cart = new Cart
        {
            Id = 1,
            UserId=3,
            VehicleId=2,
            Booking = Convert.ToDateTime("2020-11-24 12:12:00 PM"),
            Hours=3
        };

        [SetUp]
        public void Setup()
        {
            repository = new Mock<IAuthRepository<Cart>>();
            controller = new CartController(repository.Object);
        }

        [Test]
        public async Task Test_PostMethod_Return200SuccessCode()
        {
            repository.Setup(r => r.Add(cart)).ReturnsAsync(true);

            var result = await controller.Post(cart);
            var result2 = result as OkObjectResult;

            Assert.AreEqual(200, result2.StatusCode);
        }
    }
}
