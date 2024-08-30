using Azure;
using Backend.Controllers.RequestModels;
using Backend.Controllers.ResponseModels;
using Backend.DbContext;
using Backend.Service.Authentication;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.RepositoryTests
{
    public class AuthenticationRLTest
    {
        private BackendDBContext _dbContext;
        public AuthenticationRLTest()
        {
            List<User> usersInitialData = new List<User>()
            {
                new User() { Id = Guid.NewGuid(), UserName = "Test1", Password = "password1", IsAdmin=true},
                new User() { Id = Guid.NewGuid(), UserName = "Test2", Password = "password2", IsAdmin=false },
                new User() { Id = Guid.NewGuid(), UserName = "Test3", Password = "password3", IsAdmin=false }
            };

            DbContextMock<BackendDBContext> dbContextMock = new DbContextMock<BackendDBContext>(new DbContextOptionsBuilder<BackendDBContext>().Options);

            _dbContext = dbContextMock.Object;

            dbContextMock.CreateDbSetMock(temp => temp.Users, usersInitialData);
        }
        /* Login
         * Test1 : Incorrect credentials should return response with success = false
         * Test2 : Correct credentials  should return response with success = true
         */

        [Fact]
        public async void Login_IncorrectCredentials()
        {
            AuthenticationRL authenticationRL = new AuthenticationRL(_dbContext);
            LoginRequest request = new LoginRequest("InvalidUsername", "InvalidPassword");

            LoginResponse response = authenticationRL.Login(request);

            Assert.Equal(response.Success, false);

        }

        [Fact]
        public async void Login_CorrectCredentials()
        {
            AuthenticationRL authenticationRL = new AuthenticationRL(_dbContext);
            LoginRequest request = new LoginRequest("Test1", "password1");

            LoginResponse response = authenticationRL.Login(request);

            Assert.Equal(response.Success, true);

        }
    }
}
