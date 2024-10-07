using Backend.Controllers.RequestModels;
using Backend.Controllers.ResponseModels;
using Backend.DbContext;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MimeKit.Text;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Service.Authentication
{
    public class AuthenticationSL : IAuthenticationSL
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthenticationRL _authenticationRL;
        private readonly MailSettings _mailSettings = null;

        public AuthenticationSL(IConfiguration configuration, IAuthenticationRL authenticationRL)
        {
            _configuration = configuration;
            _authenticationRL = authenticationRL;
        }

        public AuthenticationSL(IAuthenticationRL authenticationRL, IOptions<MailSettings> options)
        {
            _authenticationRL = authenticationRL;
            _mailSettings = options.Value;
        }

        public AuthenticationSL(IConfiguration configuration, IAuthenticationRL authenticationRL, IOptions<MailSettings> options)
        {
            _configuration = configuration;
            _authenticationRL = authenticationRL;
            _mailSettings = options.Value;
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

        public IRecoveryPasswordResponse RecoverPassword(RecoveryRequest request)
        {
            IRecoveryPasswordResponse response = new RecoveryPasswordResponse();
            response.Success = true;
            //Password generation
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+";
            Random random = new Random();
            string password = new string(Enumerable.Repeat(validChars, 8)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            string getPass=null;

            //Check if email exists
            try
            {
                getPass = _authenticationRL.GetPassword(request.Email);
            }
            catch
            {
                response.Error = "Something went wrong";
                response.Success = false;
                return response;
            }
            if (getPass == null)
            {
                response.Success = false;
                response.Error = "Email not registered";
                return response;
            }
            //Setting up mail
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("server",_mailSettings.From));
            email.To.Add(new MailboxAddress("user", request.Email));
            email.Subject = "Password Recovery";
            email.Body = new TextPart(TextFormat.Html) { Text = "You have requested password recovery, the new password is: " + password };

            //Sending mail
            try
            {
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.SmtpServer, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.UserName, _mailSettings.Password);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                response.Error = "Something went wrong";
                response.Success = false;
            }
            return response;
            
        }

        public IRecoveryUsernameResponse RecoverUsername(RecoveryRequest request)
        {
            IRecoveryUsernameResponse response = new RecoveryUsernameResponse();
            response.Success = true;
           
            string getUserName = null;

            //Check if email exists
            try
            {
                getUserName = _authenticationRL.GetUserName(request.Email);
            }
            catch
            {
                response.Error = "Something went wrong";
                response.Success = false;
                return response;
            }
            if (getUserName == null)
            {
                response.Success = false;
                response.Error = "Email not registered";
                return response;
            }
            //Setting up mail
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("server", _mailSettings.From));
            email.To.Add(new MailboxAddress("user", request.Email));
            email.Subject = "Password Recovery";
            email.Body = new TextPart(TextFormat.Html) { Text = "You have requested username recovery, your username is: " + getUserName };

            //Sending mail
            try
            {
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.SmtpServer, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.UserName, _mailSettings.Password);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                response.Error = "Something went wrong";
                response.Success = false;
            }
            return response;

        }
    }
}
