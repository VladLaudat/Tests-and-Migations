namespace Backend.Service.Authentication
{
    public interface IServices
    {
        public string EncryptPassword(string password);
        public bool SendEmail();

    }
}
