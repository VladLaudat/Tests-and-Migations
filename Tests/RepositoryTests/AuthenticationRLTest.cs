using Azure;
using Backend.Controllers.RequestModels;
using Backend.Controllers.ResponseModels;
using Backend.DbContext;
using Backend.Service.Authentication;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Moq;
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
                new User() { Id = Guid.NewGuid(), UserName = "Test1", Password = "password1", IsAdmin=true, Email="test1@yahoo.com"},
                new User() { Id = Guid.NewGuid(), UserName = "Test2", Password = "password2", IsAdmin=false, Email = "test2@yahoo.com" },
                new User() { Id = Guid.NewGuid(), UserName = "Test3", Password = "password3", IsAdmin=false, Email = "test3@yahoo.com" }
            };

            DbContextMock<BackendDBContext> dbContextMock = new DbContextMock<BackendDBContext>(new DbContextOptionsBuilder<BackendDBContext>().Options);

            _dbContext = dbContextMock.Object;

            dbContextMock.CreateDbSetMock(temp => temp.Users, usersInitialData);
        }
        /* Login
         * Test1 : Incorrect credentials => null
         * Test2 : Correct credentials  => user
         */

        [Fact]
        public async void Login_IncorrectCredentials()
        {
            IAuthenticationRL authenticationRL = new AuthenticationRL(_dbContext);
            LoginRequest request = new LoginRequest("InvalidUsername", "InvalidPassword");

            User user = authenticationRL.Login("InvalidUsername", "InvalidPassword");

            Assert.Null(user);

        }

        [Fact]
        public async void Login_CorrectCredentials()
        {
            IAuthenticationRL authenticationRL = new AuthenticationRL(_dbContext);
            LoginRequest request = new LoginRequest("Test1", "password1");

            User user = authenticationRL.Login("Test1", "password1");

            Assert.NotNull(user);

        }
        /*Signup
         * Test1: Email or username already exists in the database => null
         * Test2: User added => User
         */

        [Fact]
        public async void Signup_EmailOrUsernameAlreadyExists()
        {
            //Arrange
            IAuthenticationRL authenticationRL = new AuthenticationRL(_dbContext);
            User user = new User() { Id = Guid.NewGuid(), IsAdmin = false, Email = "test1@yahoo.com", Password = "password1", UserName = "Test1" };
            //Act
            User userResponse = authenticationRL.Signup(user);
            //Assert
            Assert.Null(userResponse);
        }
        [Fact]
        public async void Signup_UserCorrect()
        {
            //Arrange
            IAuthenticationRL authenticationRL = new AuthenticationRL(_dbContext);
            User user = new User() { Id=Guid.NewGuid(), IsAdmin=false, Email = "test4@yahoo.com", Password = "password4", UserName = "Test4" };
            //Act
            User userResponse = authenticationRL.Signup(user);
            //Assert
            Assert.NotNull(userResponse);
        }
    }
}
