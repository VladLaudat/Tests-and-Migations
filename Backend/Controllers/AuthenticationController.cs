using Backend.Controllers.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("auth")]
    public class AuthenticationController : Controller
    {
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

            return Ok("Login successfully");
        }
    }
}