using Backend.Controllers.RequestModels;
using Backend.Controllers.ResponseModels;

namespace Backend.Service.Authentication
{
    public class AuthenticationRL : IAuthenticationRL
    {
        public LoginResponse Login(LoginRequest request)
        {
            LoginResponse response = new LoginResponse();
            response.Success = true;
            return response;
        }
    }
}
