namespace Backend.Service.Authentication
{
    public class MailSettings
    {
        public string EmailID { get; set; }
        public string From { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SmtpServer { get; set; }
        public bool UseSSL { get; set; }
        public int Port { get; set; }
    }
}
