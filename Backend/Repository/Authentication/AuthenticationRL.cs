using Backend.Controllers.RequestModels;
using Backend.Controllers.ResponseModels;
using Backend.DbContext;

namespace Backend.Service.Authentication
{
    public class AuthenticationRL : IAuthenticationRL
    {
        private readonly BackendDBContext _dBContext;

        public AuthenticationRL(BackendDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public LoginResponse Login(LoginRequest request)
        {
            LoginResponse response = new LoginResponse();

            User user = _dBContext.Users.FirstOrDefault(u => u.UserName == request.Username && u.Password == request.Password);
            
            if(user == null)
                response.Success = false;
            else
                response.Success = true;

            return response;
        }
    }
}
