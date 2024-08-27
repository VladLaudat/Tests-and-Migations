using Backend.Controllers.RequestModels;
using Backend.Controllers.ResponseModels;
using Microsoft.AspNetCore.Identity.Data;

namespace Backend.Service.Authentication
{
    public interface IAuthenticationSL
    {
        public LoginResponse Login(Backend.Controllers.RequestModels.LoginRequest request);

    }
}
