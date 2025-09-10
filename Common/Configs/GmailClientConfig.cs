namespace Ivoluntia.BackgroudServices.Common.Configs
{
    public class GmailClientConfig
    {
        public string FromEmail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string SmtpServer { get; set; }
        public int PortNumber { get; set; }
        public bool EnableSSL { get; set; }
    }
}
