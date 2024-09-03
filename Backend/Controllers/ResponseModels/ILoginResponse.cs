namespace Backend.Controllers.ResponseModels
{
    public interface ILoginResponse
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
