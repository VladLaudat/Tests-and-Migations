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

            configuration = mockConfiguration.Object;
        }

        /*Login
         * Test1 : Exception regarding dbContext =>  Response.ErrorMessage("Something went wrong")
         * Test2 : Credentials wrong, user null => Response.ErrorMessage("Invalid credentials")
         * Test3 : Credentials correct => Response.success(true) 
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
        /*Signup
         * Test1 : Exception regarding dbContext =>  Response.ErrorMessage("Something went wrong")
         * Test2 : Request wrong, user null => Response.ErrorMessage("Email or password already registered")
         * Test3 : Request correct => Response.success(true) 
         */

        [Fact]
        public async void Signup_dbContextException()
        {
            SignupRequest signupRequest = new SignupRequest()
            {
                Username = "Test1",
                Password = "password1",
                Email = "test1@yahoo.com"
            };
            _dbContext = null;
            IAuthenticationRL authenticationRL = new AuthenticationRL(_dbContext);
            IAuthenticationSL authenticationSL = new AuthenticationSL(configuration, authenticationRL);

            ISignupResponse signupResponse = authenticationSL.Signup(signupRequest);

            Assert.Equal(signupResponse.Success, false);
            Assert.Equal(signupResponse.ErrorMessage, "Something went wrong");
        }
        [Fact]
        public async void Signup_EmailOrPasswordAlreadyRegistered()
        {
            SignupRequest signupRequest = new SignupRequest()
            {
                Username = "Test1",
                Password = "password1",
                Email = "test1@yahoo.com"
            };
            IAuthenticationRL authenticationRL = new AuthenticationRL(_dbContext);
            IAuthenticationSL authenticationSL = new AuthenticationSL(configuration, authenticationRL);

            ISignupResponse signupResponse = authenticationSL.Signup(signupRequest);

            Assert.Equal(signupResponse.Success, false);
            Assert.Equal(signupResponse.ErrorMessage, "Email or password already registered");
        }
        [Fact]
        public async void Signup_RequestCorrect()
        {
            SignupRequest signupRequest = new SignupRequest()
            {
                Username = Guid.NewGuid().ToString("N").Substring(0,5),
                Password = Guid.NewGuid().ToString("N").Substring(0, 8),
                Email = Guid.NewGuid().ToString("N").Substring(0, 5) + "@yahoo.com"
            };
            IAuthenticationRL authenticationRL = new AuthenticationRL(_dbContext);
            IAuthenticationSL authenticationSL = new AuthenticationSL(configuration, authenticationRL);

            ISignupResponse signupResponse = authenticationSL.Signup(signupRequest);

            Assert.Equal(signupResponse.Success, true);
        }
    }
}
