namespace Backend.Controllers.ResponseModels
{
    public class SignupResponse : ISignupResponse
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
