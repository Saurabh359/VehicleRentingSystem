using AuthorizationService.Controllers;
using AuthorizationService.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using VehicleService.Repository;

namespace AuthorizationServiceNUnitTesting
{
    [TestFixture]
    public class UserControllerTesting
    {
        private Mock<IAuthRepository<User>> repository;
        private UserController controller;

        User user = new User { Id = 2,
                               Name = "Saurabh",
                               Gender = "Male",
                               Password = "hello",
                               Email = "s123@gmail.com",
                               PhoneNo = "9865347123",
                               Address = "Haldwani" };

        [SetUp]
        public void Setup()
        {
            repository = new Mock<IAuthRepository<User>>();
            controller = new UserController(repository.Object);
        }

        [Test]
        public async Task Test_PostMethod_Return200SuccessCode()
        {
            repository.Setup(r => r.Add(user)).ReturnsAsync(true);

            var result =await controller.Post(user);
            var result2 = result as OkObjectResult;

            Assert.AreEqual(200, result2.StatusCode);
        }
    }
}