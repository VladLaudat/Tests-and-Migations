namespace Backend.Controllers.ResponseModels
{
    public class RecoveryPasswordResponse : IRecoveryPasswordResponse
    {
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}
