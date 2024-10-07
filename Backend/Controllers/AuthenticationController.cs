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
            if (request == null)
                return BadRequest("Object null");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
            }

            ISignupResponse signupResponse = _authenticationSL.Signup(request);

            if(signupResponse.Success == false)
                return BadRequest(signupResponse.ErrorMessage);

            return Ok(signupResponse);
        }
        [Route("recoverPassword")]
        public async Task<IActionResult> RecoverPassword([FromForm] RecoveryRequest request)
        {
            if (request == null || request.Email ==null)
                return BadRequest("Email null");

            if (!ModelState.IsValid)
            {
                return BadRequest(string.Join(",",ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)));
            }
            IRecoveryPasswordResponse response = _authenticationSL.RecoverPassword(request);
            if (!response.Success)
                return BadRequest(response.Error);
            return Ok(response);
        }
        [Route("recoverUsername")]
        public async Task<IActionResult> RecoverUsername([FromForm] RecoveryRequest request)
        {
            if (request == null || request.Email==null)
                return BadRequest("Email null");

            if (!ModelState.IsValid)
            {
                return BadRequest(string.Join(",",ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)));
            }
            IRecoveryUsernameResponse response = _authenticationSL.RecoverUsername(request);
            if (!response.Success)
                return BadRequest(response.Error);
            return Ok(response);
        }
    }
}