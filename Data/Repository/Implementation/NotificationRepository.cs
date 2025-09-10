using Ivoluntia.BackgroudServices.Data.Repository.Interface;

namespace Ivoluntia.BackgroudServices.Data.Repository.Implementation
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly iVoluntiaDataContext _context;

        public NotificationRepository(iVoluntiaDataContext context)
        {
            _context = context;
        }

        public async Task<NotificationChannelSettings> GetChannelSettings(string provider, string channelName)
        {
            var channel = await _context.NotificationChannels
                .Include(c => c.ChannelSettings)
                .FirstOrDefaultAsync(c => c.ChannelName.ToLower() == channelName.ToLower());

            if (channel == null)
                throw new InvalidOperationException($"Notification channel {channel.ChannelName} not found.");

            var channelSettings = channel.ChannelSettings
                .FirstOrDefault(cs => cs.IsActive && cs.Provider.ToLower() == provider.ToLower());

            if (channelSettings == null)
                throw new InvalidOperationException($"No Active settings found for {channel.ChannelName} channel found.");
            return channelSettings;
        }
    }
}
