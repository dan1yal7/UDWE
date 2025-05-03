using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniDwe.AutoMapper;
using UniDwe.Controllers;
using UniDwe.Infrastructure;
using UniDwe.Models;
using UniDwe.Models.ViewModel;
using UniDwe.Services;
using Xunit;

namespace RegistrationUnitTest
{
    public class RegistrationControllerTest
    {
        [Fact]
        public async Task RegistrationUserARedirect()
        {
            //Arrange
            var mock = new Mock<IRegistrationSerivce>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "SuccessfulRegistrationDb").Options;
            var context = new ApplicationDbContext(options);
            var controller = new RegistrationController(mock.Object, context);
            RegistrationViewModel registrationViewModel = new RegistrationViewModel()
            {
                UserName = "Bruce Wayne",
                Email = "bruce@wayne.com",
                Password = "1234567890"
            };

            //Act 
            var result = await controller.IndexSave(registrationViewModel);

            //Assert
            var redirect = Assert.IsType<RedirectResult>(result);
            Assert.NotNull(redirect);
            Assert.Equal("/", redirect.Url);
            mock.Verify(u => u.CreateUserAsync(It.Is<User>(user => user.UserName == registrationViewModel.UserName &&
            user.Email == registrationViewModel.Email &&
            user.PasswordHash == registrationViewModel.Password)), Times.Once); ;
        }

        [Fact]
        public async Task ReturnsViewResultWithErrorIfUserDidNotPassValidation()
        {
            // Arrange
            var mock = new Mock<IRegistrationSerivce>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb2") 
                .Options;

            var context = new ApplicationDbContext(options);

            var controller = new RegistrationController(mock.Object, context);
            controller.ModelState.AddModelError("Username", "Username is required");
            controller.ModelState.AddModelError("Email", "Email is required");
            controller.ModelState.AddModelError("Password", "Password is required");

            RegistrationViewModel registrationViewModel = new RegistrationViewModel()
            {
                UserName = "a", // слишком короткий username
                Email = "invalid@bad", // невалидный email
                Password = "123" // слишком короткий пароль
            };

            // Act
            var result = await controller.IndexSave(registrationViewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(registrationViewModel, viewResult?.Model);
 
        }
    }
}
