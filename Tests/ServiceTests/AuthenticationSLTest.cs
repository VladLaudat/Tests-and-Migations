using Backend.Controllers.RequestModels;
using Backend.Controllers.ResponseModels;
using Backend.DbContext;
using Backend.Service.Authentication;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.ServiceTests
{
    public class AuthenticationSLTest
    {
        private BackendDBContext _dbContext;
        private Microsoft.Extensions.Configuration.IConfiguration configuration = new ConfigurationBuilder().Build();
        public AuthenticationSLTest()
        {
            List<User> usersInitialData = new List<User>()
            {
                new User() { Id = Guid.NewGuid(), UserName = "Test1", Password = "password1"},
                new User() { Id = Guid.NewGuid(), UserName = "Test2", Password = "password2" },
                new User() { Id = Guid.NewGuid(), UserName = "Test3", Password = "password3" }
            };

            DbContextMock<BackendDBContext> dbContextMock = new DbContextMock<BackendDBContext>(new DbContextOptionsBuilder<BackendDBContext>().Options);

            _dbContext = dbContextMock.Object;

            dbContextMock.CreateDbSetMock(temp => temp.Users, usersInitialData);

            var mockConfiguration = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            mockConfiguration.SetupGet(x => x["Jwt:Issuer"]).Returns("your-issuer");
            mockConfiguration.SetupGet(x => x["Jwt:Audience"]).Returns("your-audience");
            mockConfiguration.SetupGet(x => x["Jwt:ExpiresInDays"]).Returns("7");
            mockConfiguration.SetupGet(x => x["Jwt:Key"]).Returns("AuthenticationSecretKeyDuringTesting");

            configuration = mockConfiguration.Object;
        }

        /*Login
         * Test1 : Exception regarding dbContext returns response with error "Something went wrong"
         * Test2 : Credentials wrong, user null, returns response with error "Invalid credentials"
         * Test3 : Credentials correct, returns response with success=true 
         */

        [Fact]
        public async void Login_dbContextException()
        {
            LoginRequest loginRequest = new LoginRequest()
            {
                Username = "username",
                Password = "password"
            };
            _dbContext = null;
            IAuthenticationRL authenticationRL = new AuthenticationRL(_dbContext);
            IAuthenticationSL authenticationSL = new AuthenticationSL(configuration, authenticationRL);

            ILoginResponse loginResponse = authenticationSL.Login(loginRequest);

            Assert.Equal(loginResponse.ErrorMessage, "Something went wrong");
        }

        [Fact]
        public async void Login_CredentialsWrong()
        {
            LoginRequest loginRequest = new LoginRequest()
            {
                Username = "username",
                Password = "password"
            };
            IAuthenticationRL authenticationRL = new AuthenticationRL(_dbContext);
            IAuthenticationSL authenticationSL = new AuthenticationSL(configuration, authenticationRL);

            ILoginResponse loginResponse = authenticationSL.Login(loginRequest);

            Assert.Equal(loginResponse.ErrorMessage, "Invalid credentials");
        }

        [Fact]
        public async void Login_CredentialsCorrect()
        {
            LoginRequest loginRequest = new LoginRequest()
            {
                Username = "Test1",
                Password = "password1"
            };
            IAuthenticationRL authenticationRL = new AuthenticationRL(_dbContext);
            IAuthenticationSL authenticationSL = new AuthenticationSL(configuration, authenticationRL);

            ILoginResponse loginResponse = authenticationSL.Login(loginRequest);

            Assert.Equal(loginResponse.Success, true);
        }
    }
}
