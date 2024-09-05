using Backend.Controllers.RequestModels;
using Backend.Controllers.ResponseModels;
using Backend.Service.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("auth")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationSL _authenticationSL;

        public AuthenticationController(IAuthenticationSL authenticationSL)
        {
            _authenticationSL = authenticationSL;
        }

        [Route("login")]
        public async Task<IActionResult> Login([FromForm] LoginRequest request)
        {
            if (request == null)
            {
                return BadRequest("Object null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
            }

            ILoginResponse loginResponse = _authenticationSL.Login(request);

            if (loginResponse.Success == false)
                return BadRequest(loginResponse.ErrorMessage);

            return Ok(loginResponse);
        }

        [Route("signup")]
        public async Task<IActionResult> Signup([FromForm] SignupRequest request)
        {
            throw new NotImplementedException();
        }
    }
}