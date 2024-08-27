using Backend.Controllers.RequestModels;
using Backend.Controllers.ResponseModels;

namespace Backend.Service.Authentication
{
    public class AuthenticationSL : IAuthenticationSL
    {
        private readonly IAuthenticationRL _authenticationRL;

        public AuthenticationSL(IAuthenticationRL authenticationRL)
        {
            _authenticationRL = authenticationRL;
        }
        public LoginResponse Login(LoginRequest request)
        {
            LoginResponse response = _authenticationRL.Login(request);

            return response;
        }
    }
}
