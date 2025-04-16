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
            
            //Act
            var result = await controller.LogIn(model);

            // Assert
            var redirect = Assert.IsType<RedirectResult>(result);
            Assert.Equal("/", redirect.Url);
        }
    }
}
