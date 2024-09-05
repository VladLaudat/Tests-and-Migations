namespace Backend.Controllers.ResponseModels
{
    public interface ISignupResponse
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
