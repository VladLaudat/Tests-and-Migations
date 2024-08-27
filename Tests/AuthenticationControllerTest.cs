using Backend.Controllers;
using Backend.Controllers.RequestModels;
using Backend.Service.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Tests
{
    public class AuthenticationControllerTest
    {
        /*Login
         * Test1: LoginRequest is null it should return badRequest("Object null")
         * Test2: Username/password is null/empty it should return badRequest("Username/password null")
         * Test3: Credentials incorrect it should return badRequest
         * Test4: Username/password are correct it should return Ok("Login successfully")
         */
        [Fact]
        public async void Login_LoginRequestNull()
        {
            //Arrange
            LoginRequest loginRequest =null;
            IAuthenticationRL authenticationRL = new AuthenticationRL();
            IAuthenticationSL authenticationSL = new AuthenticationSL(authenticationRL);
            AuthenticationController controller = new AuthenticationController(authenticationSL);
            //Act
            IActionResult result = await controller.Login(loginRequest);
            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void Login_LoginRequestUsernamePasswordNullorEmpty()
        {
            //Arrange
            LoginRequest loginRequest = new LoginRequest();
            IAuthenticationRL authenticationRL = new AuthenticationRL();
            IAuthenticationSL authenticationSL = new AuthenticationSL(authenticationRL);
            AuthenticationController controller = new AuthenticationController(authenticationSL);
            controller.ModelState.AddModelError("UsernamePasswordNullorEmpty", "Username or password required");
            //Act
            IActionResult result = await controller.Login(loginRequest);
            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void Login_LoginRequestCorrect()
        {
            //Arrange
            LoginRequest loginRequest = new LoginRequest("user", "pass");
            IAuthenticationRL authenticationRL = new AuthenticationRL();
            IAuthenticationSL authenticationSL = new AuthenticationSL(authenticationRL);
            AuthenticationController controller = new AuthenticationController(authenticationSL);
            //Act
            IActionResult result = await controller.Login(loginRequest);
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}