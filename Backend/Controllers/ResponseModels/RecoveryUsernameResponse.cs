namespace Backend.Controllers.ResponseModels
{
    public class RecoveryUsernameResponse : IRecoveryUsernameResponse
    {
        public string Error { get; set; }
        public bool Success { get; set; }
    }
}
