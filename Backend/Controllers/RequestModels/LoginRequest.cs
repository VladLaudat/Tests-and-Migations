using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Backend.Controllers.RequestModels
{
    public class LoginRequest
    {
        public LoginRequest()
        {
            Username = null;
            Password = null;
        }

        public LoginRequest(string Username, string Password)
        {
            this.Username=Username;
            this.Password=Password;
        }

        [Required(ErrorMessage = "Username is required", AllowEmptyStrings = false)]
        [NotNull]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required", AllowEmptyStrings = false)]
        [NotNull]
        public string Password { get; set; }
    }
}
