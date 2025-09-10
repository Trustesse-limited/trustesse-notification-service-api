using Ivoluntia.BackgroudServices.Common.Configs;
using Ivoluntia.BackgroudServices.Common.Dtos;
using Ivoluntia.BackgroudServices.Common.Helpers;
using Ivoluntia.BackgroudServices.Data.Enums;
using Ivoluntia.BackgroudServices.Data.Repository.Interface;
using System.Net.Mail;

namespace Ivoluntia.BackgroudServices.Services.Implementations
{
    public class GmailService : IEmailService
    {
        private readonly INotificationRepository _notificationRepository;

        public GmailService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<bool> SendEmail(EmailParams model)
        {
            try
            {
                var channelSettings = await _notificationRepository.GetChannelSettings(EmailServiceProvider.Gmail.ToString(), NotificationChannels.Email.ToString());
                GmailClientConfig config = (GmailClientConfig)NotificationSettingsFactory.DeserializeProviderSettings(channelSettings.Settings, channelSettings.Provider);

                using var mail = new MailMessage
                {
                    From = new MailAddress(config.FromEmail, config.DisplayName),
                    Subject = model.Subject,
                    Body = model.Message,
                    IsBodyHtml = true
                };

                foreach (var item in model.Receivers)
                {
                    mail.To.Add(item);
                }

                if (!string.IsNullOrEmpty(model.Attachments))
                {
                    mail.Attachments.Add(new Attachment(model.Attachments));
                }

                using var client = new SmtpClient(config.SmtpServer, config.PortNumber)
                {
                    Credentials = new NetworkCredential(config.FromEmail, config.Password),
                    EnableSsl = config.EnableSSL
                };

                await client.SendMailAsync(mail);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
