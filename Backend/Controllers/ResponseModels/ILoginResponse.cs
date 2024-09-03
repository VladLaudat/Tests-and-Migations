namespace Backend.Controllers.ResponseModels
{
    public interface ILoginResponse : IAuthenticationToken
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
