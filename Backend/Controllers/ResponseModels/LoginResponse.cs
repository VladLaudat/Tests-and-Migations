namespace Backend.Controllers.ResponseModels
{
    public class LoginResponse : ILoginResponse
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
    }
}
