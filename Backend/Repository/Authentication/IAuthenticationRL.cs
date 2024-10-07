using Backend.Controllers.RequestModels;
using Backend.Controllers.ResponseModels;
using Backend.DbContext;

namespace Backend.Service.Authentication
{
    public interface IAuthenticationRL
    {
        public User Login(string Username, string Password);
        public User Signup(User user);
        public string GetPassword(string email);
        public string GetUserName(string email);
    }
}
