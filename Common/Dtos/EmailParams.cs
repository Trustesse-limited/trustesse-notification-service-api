namespace Ivoluntia.BackgroudServices.Common.Dtos
{
    public class EmailParams
    {
        public List<string> Receivers { get; set; } = new List<string>();
        public string? Attachments { get; set; }
        public string Subject { get; set; } = "iVoluntia Notification";
        public string Message { get; set; } = string.Empty;
    }
}
