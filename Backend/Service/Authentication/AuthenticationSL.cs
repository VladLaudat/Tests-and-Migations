using Backend.Controllers.RequestModels;
using Backend.Controllers.ResponseModels;
using Backend.DbContext;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Service.Authentication
{
    public class AuthenticationSL : IAuthenticationSL
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthenticationRL _authenticationRL;

        public AuthenticationSL(IConfiguration configuration, IAuthenticationRL authenticationRL)
        {
            _configuration = configuration;
            _authenticationRL = authenticationRL;
        }
        public LoginResponse Login(LoginRequest request)
        {
            User user = _authenticationRL.Login(request.Username, request.Password);

            if(user!=null)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])); //takes the key from appsettings.json
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); // specifies the algorithm and key for encryption

                var secureToken = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    new List<Claim>(),
                  expires: DateTime.Now.AddDays(Convert.ToInt32(_configuration["Jwt:ExpiresInDays"])),
                  signingCredentials: credentials
                    );
            }

            return new LoginResponse();
        }
    }
}
