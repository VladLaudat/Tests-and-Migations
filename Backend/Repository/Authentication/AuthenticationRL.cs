using Backend.Controllers.RequestModels;
using Backend.Controllers.ResponseModels;
using Backend.DbContext;
using Microsoft.Data.SqlClient;

namespace Backend.Service.Authentication
{
    public class AuthenticationRL : IAuthenticationRL
    {
        private readonly BackendDBContext _dBContext;

        public AuthenticationRL(BackendDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public User Login(string Username, string Password)
        {
            User user = _dBContext.Users.FirstOrDefault(u => u.UserName == Username && u.Password == Password);

            return user;
        }

        public User Signup(User user)
        {
            throw new NotImplementedException();
        }
    }
}
