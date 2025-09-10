using Ivoluntia.BackgroudServices.Common.Dtos;
using Ivoluntia.BackgroudServices.Data.Repository.Interface;

namespace Ivoluntia.BackgroudServices.Services.Background
{
    public class EmailJobService : IEmailJobService
    {
        private readonly IUnitofWork _uow;
        private readonly INotificationService _notificationService;
        private readonly IEmailServiceFactory _emailServiceFactory;

        public EmailJobService(INotificationService notificationService, IEmailServiceFactory emailServiceFactory, IUnitofWork uow)
        {
            _notificationService = notificationService;
            _emailServiceFactory = emailServiceFactory;
            _uow = uow;
        }

        [AutomaticRetry(Attempts = 3, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        public async Task<int> SendNotificationEmailAsync()
        {
            var unsentEmails = await _notificationService.GetUnsentEmailsAsync();

            if (!unsentEmails.Any())
                return 0;

            var emailService = _emailServiceFactory.GetEmailService();

            foreach (var item in unsentEmails)
            {
                bool sent = await emailService.SendEmail(new EmailParams
                {
                    Receivers = item.Email.Split(',').ToList(),
                    Subject = item.Subject ?? "iVoluntia Notification",
                    Message = item.MessageBody,
                });
                if (sent)
                    item.IsSent = true;
                else
                    item.RetryCount += 1;
            }

            _notificationService.UpdateNotificationStatuses(unsentEmails);

            await _uow.SaveChangesAsync();

            return unsentEmails.Count;
        }


        public async Task<ApiResponse<object>> SendEmailAsync(SendEmailRequest request)
        {
            if (request.Receivers == null || !request.Receivers.Any())
                return ApiResponse<object>.Failure(StatusCodes.Status400BadRequest, "At least one recipient is required.");

            var emailService = _emailServiceFactory.GetEmailService();

            var result = await emailService.SendEmail(new EmailParams
            {
                Receivers = request.Receivers,
                Subject = request.Subject ?? "iVoluntia Notification",
                Message = request.Message,
                Attachments = request.Attachments
            });

            if (result)
                return ApiResponse<object>.Success(
                    "Email sent successfully!",
                    new
                    {
                        recipients = request.Receivers,
                        subject = request.Subject ?? "iVoluntia Notification",
                        attachment = request.Attachments
                    }
                );

            return ApiResponse<object>.Failure(StatusCodes.Status500InternalServerError, "Failed to send email.");
        }
    }
}

