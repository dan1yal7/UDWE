using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniDwe.Controllers;
using UniDwe.Helpers;
using UniDwe.Models;
using UniDwe.Models.ViewModel;
using UniDwe.Services;
using Xunit;

namespace RegistrationUnitTest
{
    public class LoginControllerTest
    { 
        [Fact]
        public async Task GetUserByEmailReturnAuthorizedIfUserFound()
        {
            //Arrange 
            var mock = new Mock<IRegistrationSerivce>();
            var passwordHelper = new PasswordHelper();
            var controller = new LoginController(mock.Object, passwordHelper);

            var email = "test@email.com";
            var password = "123456789";
            var salt = "random_salt";
            var hashed = passwordHelper.HashPassword(password, salt);

            mock.Setup(x => x.GetUserByEmailAsync(email)).ReturnsAsync(new User
            {
                Email = email,
                PasswordHash = hashed,
                Salt = salt
            });
            mock.Setup(x => x.AuthenticateUserAsync(email, password, false));

            var model = new LoginViewModel
            {
                Email = email,
                Password = password,
                RememberMe = false
            };

            var urlHelperMock = new Mock<IUrlHelper>();
            urlHelperMock.Setup(x => x.IsLocalUrl(It.IsAny<string>())).Returns(true);
            controller.Url = urlHelperMock.Object;

            //Act
            var result = await controller.LogIn(model);

            // Assert
            var redirect = Assert.IsType<RedirectResult>(result);
            Assert.Equal("/", redirect.Url);
        }

        [Fact]
        public async Task GetUserByEmailReturnErrorIfPasswordIsIncorrect()
        {
            //Arrange
            var mock = new Mock<IRegistrationSerivce>();
            var passwordHelper = new PasswordHelper();
            var controller = new LoginController(mock.Object, passwordHelper);

            var email = "test@email.com";
            var password = "wrong_password";
            var salt = "random_salt";
            var hashed = passwordHelper.HashPassword("correct_password", salt);

            mock.Setup(x => x.GetUserByEmailAsync(email)).ReturnsAsync(new User
            {
                Email = email,
                PasswordHash = hashed,
                Salt = salt
            });

            var model = new LoginViewModel
            {
                Email = email,
                Password = password,
                RememberMe = false
            };

            //Act 
            var result = await controller.LogIn(model);

            //Assert 
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(model, viewResult?.Model);
            Assert.False(controller.ModelState.IsValid);

        }

        [Fact] 
        public async Task GetUserByEmailReturnNotRegisteredOrNotFoundIfGmailIsNotFound()
        {
            //Arrange
            var mock = new Mock<IRegistrationSerivce>();
            var passwordHelper = new PasswordHelper();
            var controller = new LoginController(mock.Object, passwordHelper);

            var email = "notfoundTest@gmai.com";
            var password = "1234567899";

            mock.Setup(x => x.GetUserByEmailAsync(email)).ReturnsAsync((User)null!);

            var model = new LoginViewModel
            {
                Email = email,
                Password = password,
            };

            //Act 
            var result = await controller.LogIn(model);

            //Assert 
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(model, viewResult?.Model);
            Assert.False(controller.ModelState.IsValid);
        }
    }
}
