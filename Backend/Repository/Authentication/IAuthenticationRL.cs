using Backend.Controllers.RequestModels;
using Backend.Controllers.ResponseModels;

namespace Backend.Service.Authentication
{
    public interface IAuthenticationRL
    {
        public LoginResponse Login(LoginRequest request);
    }
}
