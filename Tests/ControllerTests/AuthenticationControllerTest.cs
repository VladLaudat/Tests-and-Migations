using Backend.Controllers;
using Backend.Controllers.RequestModels;
using Backend.DbContext;
using Backend.Service.Authentication;
using Castle.Core.Configuration;
using EntityFrameworkCoreMock;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Tests.ControllerTests
{
    public class AuthenticationControllerTest
    {
        private BackendDBContext _dbContext;
        private Microsoft.Extensions.Configuration.IConfiguration configuration = new ConfigurationBuilder().Build();
        public AuthenticationControllerTest()
        {
            List<User> usersInitialData = new List<User>()
            {
                new User() { Id = Guid.NewGuid(), UserName = "Test1", Password = "password1", IsAdmin=true, Email="test1@yahoo.com"},
                new User() { Id = Guid.NewGuid(), UserName = "Test2", Password = "password2", IsAdmin=false, Email = "test2@yahoo.com" },
                new User() { Id = Guid.NewGuid(), UserName = "Test3", Password = "password3", IsAdmin=false, Email = "test3@yahoo.com" }
            };

            DbContextMock<BackendDBContext> dbContextMock = new DbContextMock<BackendDBContext>(new DbContextOptionsBuilder<BackendDBContext>().Options);

            _dbContext = dbContextMock.Object;

            dbContextMock.CreateDbSetMock(temp => temp.Users, usersInitialData);

            var mockConfiguration = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            mockConfiguration.SetupGet(x => x["Jwt:Issuer"]).Returns("your-issuer");
            mockConfiguration.SetupGet(x => x["Jwt:Audience"]).Returns("your-audience");
            mockConfiguration.SetupGet(x => x["Jwt:ExpiresInDays"]).Returns("7");
            mockConfiguration.SetupGet(x => x["Jwt:Key"]).Returns("AuthenticationSecretKeyDuringTesting");

            configuration=mockConfiguration.Object;
        }


        /*Login
         * Test1: LoginRequest is null => badRequest("Object null")
         * Test2: Username/password is null/empty => badRequest
         * Test3: Credentials incorrect => badRequest
         * Test4: Username/password are correct => Ok("Login successfully")
         */


        [Fact]
        public async void Login_LoginRequestNull()
        {
            //Arrange
            Backend.Controllers.RequestModels.LoginRequest loginRequest = null;
            IAuthenticationRL authenticationRL = new AuthenticationRL(_dbContext);
            IAuthenticationSL authenticationSL = new AuthenticationSL(configuration, authenticationRL);
            AuthenticationController controller = new AuthenticationController(authenticationSL);
            //Act
            IActionResult result = await controller.Login(loginRequest);
            //Assert
            Assert.IsType<BadRequestObjectResult>(result);

            if (result is BadRequestObjectResult badRequest)
                Assert.Equal(badRequest.Value, "Object null");
        }

        [Fact]
        public async void Login_LoginRequestUsernamePasswordNullorEmpty()
        {
            //Arrange
            Backend.Controllers.RequestModels.LoginRequest loginRequest = new Backend.Controllers.RequestModels.LoginRequest();
            IAuthenticationRL authenticationRL = new AuthenticationRL(_dbContext);
            IAuthenticationSL authenticationSL = new AuthenticationSL(configuration, authenticationRL);
            AuthenticationController controller = new AuthenticationController(authenticationSL);
            controller.ModelState.AddModelError("UsernamePasswordNullorEmpty", "Username or password required");
            //Act
            IActionResult result = await controller.Login(loginRequest);
            //Assert
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public async void Login_LoginRequestIncorrectCredentials()
        {
            //Arrange
            Backend.Controllers.RequestModels.LoginRequest loginRequest = new Backend.Controllers.RequestModels.LoginRequest("user", "pass");
            IAuthenticationRL authenticationRL = new AuthenticationRL(_dbContext);
            IAuthenticationSL authenticationSL = new AuthenticationSL(configuration, authenticationRL);
            AuthenticationController controller = new AuthenticationController(authenticationSL);
            //Act
            IActionResult result = await controller.Login(loginRequest);
            //Assert
            Assert.IsType<BadRequestObjectResult>(result);

            if (result is BadRequestObjectResult badRequest)
                Assert.Equal(badRequest.Value, "Invalid credentials");
        }

        [Fact]
        public async void Login_LoginRequestCorrect()
        {
            //Arrange
            Backend.Controllers.RequestModels.LoginRequest loginRequest = new Backend.Controllers.RequestModels.LoginRequest("Test1", "password1");
            IAuthenticationRL authenticationRL = new AuthenticationRL(_dbContext);
            IAuthenticationSL authenticationSL = new AuthenticationSL(configuration, authenticationRL);
            AuthenticationController controller = new AuthenticationController(authenticationSL);
            //Act
            IActionResult result = await controller.Login(loginRequest);
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        /*Signup
         * Test1: SignupRequest is null => badRequest("Object null")
         * Test2: Username/password/Email is null/empty => badRequest
         * Test3: Email adress is invalid => badRequest
         * Test3: Username/password/Email are correct => Ok("Signup successfully")
         */

        [Fact]
        public async void Signup_RequestNull()
        {
            //Arrange
            SignupRequest signupRequest = null;
            IAuthenticationRL authenticationRL = new AuthenticationRL(_dbContext);
            IAuthenticationSL authenticationSL = new AuthenticationSL(configuration, authenticationRL);
            AuthenticationController controller = new AuthenticationController(authenticationSL);
            //Act
            IActionResult result = await controller.Signup(signupRequest);
            //Assert
            Assert.IsType<BadRequestObjectResult>(result);

            if (result is BadRequestObjectResult badRequest)
                Assert.Equal(badRequest.Value, "Object null");
        }

        [Fact]
        public async void Signup_RequestParamsNullOrEmpty()
        {
            //Arrange
            SignupRequest signupRequest = new SignupRequest();
            IAuthenticationRL authenticationRL = new AuthenticationRL(_dbContext);
            IAuthenticationSL authenticationSL = new AuthenticationSL(configuration, authenticationRL);
            AuthenticationController controller = new AuthenticationController(authenticationSL);
            controller.ModelState.AddModelError("ParamsNullorEmpty", "Make sure you completed every field");
            //Act
            IActionResult result = await controller.Signup(signupRequest);
            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void Signup_EmailInvalid()
        {
            //Arrange
            SignupRequest signupRequest = new SignupRequest();
            IAuthenticationRL authenticationRL = new AuthenticationRL(_dbContext);
            IAuthenticationSL authenticationSL = new AuthenticationSL(configuration, authenticationRL);
            AuthenticationController controller = new AuthenticationController(authenticationSL);
            controller.ModelState.AddModelError("ParamsNullorEmpty", "Make sure you completed every field");
            //Act
            IActionResult result = await controller.Signup(signupRequest);
            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void Signup_RequestCorrect()
        {
            //Arrange
            SignupRequest signupRequest = new SignupRequest() { Email = "testemail@yahoo.com", Password = "testpassword", Username="test"};
            IAuthenticationRL authenticationRL = new AuthenticationRL(_dbContext);
            IAuthenticationSL authenticationSL = new AuthenticationSL(configuration, authenticationRL);
            AuthenticationController controller = new AuthenticationController(authenticationSL);

            //Act
            IActionResult result = await controller.Signup(signupRequest);

            //Assert
            Assert.IsType<OkObjectResult>(result);

        }
    }
}