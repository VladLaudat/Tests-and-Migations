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
        public ILoginResponse Login(LoginRequest request)
        {
            ILoginResponse response = new LoginResponse();
            response.Success = false;
            response.ErrorMessage = "Invalid credentials";
            User user = null;

            try
            {
                user = _authenticationRL.Login(request.Username, request.Password);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Something went wrong";
                return response;
            }


            if (user != null)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])); //takes the key from appsettings.json
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); // specifies the algorithm and key for encryption

                var secureToken = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    new List<Claim>() { new Claim("isAdmin", user.IsAdmin.ToString()) },
                  expires: DateTime.Now.AddDays(Convert.ToInt32(_configuration["Jwt:ExpiresInDays"])),
                  signingCredentials: credentials
                    );

                response.ErrorMessage = null;
                response.Success = true;
                response.Token = new JwtSecurityTokenHandler().WriteToken(secureToken);

            }

            return response;
        }

        public ISignupResponse Signup(SignupRequest request)
        {
            ISignupResponse response = new SignupResponse();
            response.Success = false;
            response.ErrorMessage = "Email or username already registered";
            User userResponse = null;

            User user = new User() { Id=Guid.NewGuid(), Email = request.Email, Password = request.Password, UserName = request.Username, IsAdmin=false};

            try
            {
                userResponse = _authenticationRL.Signup(user);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Something went wrong";
                return response;
            }

            if(userResponse != null)
            {
                response.Success = true;
                response.ErrorMessage = null;
            }

            return response;
        }
    }
}
