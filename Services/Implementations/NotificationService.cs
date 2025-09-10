using Ivoluntia.BackgroudServices.Data.Enums;

namespace Ivoluntia.BackgroudServices.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly iVoluntiaDataContext _context;

        public NotificationService(iVoluntiaDataContext context)
        {
            _context = context;
        }

        public async Task<List<Notification>> GetUnsentEmailsAsync()
        {
            var unsentEmails = await _context.Notifications
                .Where(n => !n.IsSent && n.Channel == NotificationChannels.Email.ToString() && n.RetryCount < 5)
                .OrderByDescending(n => n.Priority)
                .ToListAsync();

            return unsentEmails;
        }

        public void UpdateNotificationStatuses(IEnumerable<Notification> notifications)
        {
            _context.Notifications.UpdateRange(notifications);

        }
    }
}
