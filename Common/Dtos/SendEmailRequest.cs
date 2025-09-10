namespace Ivoluntia.BackgroudServices.Common.Dtos
{
    public class SendEmailRequest
    {
        public List<string> Receivers { get; set; } = new List<string>();
        public string? Attachments { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
