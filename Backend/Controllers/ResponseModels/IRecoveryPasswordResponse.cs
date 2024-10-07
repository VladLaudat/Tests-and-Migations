namespace Backend.Controllers.ResponseModels
{
    public interface IRecoveryPasswordResponse
    {
        
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}
