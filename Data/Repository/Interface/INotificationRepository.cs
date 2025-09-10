
namespace Ivoluntia.BackgroudServices.Data.Repository.Interface
{
    public interface INotificationRepository
    {
        Task<NotificationChannelSettings> GetChannelSettings(string provider, string channelName);
    }
}
