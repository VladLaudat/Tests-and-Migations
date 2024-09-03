using Backend.Controllers.RequestModels;
using Backend.Controllers.ResponseModels;
using Microsoft.AspNetCore.Identity.Data;

namespace Backend.Service.Authentication
{
    public interface IAuthenticationSL
    {
        public ILoginResponse Login(Backend.Controllers.RequestModels.LoginRequest request);

    }
}
