
namespace Ivoluntia.BackgroudServices.Data.Repository.Interface
{
    public interface INotificationRepository : IGenericRepository<Notification>
    {
        Task<NotificationChannelSettings> GetChannelSettings(string provider, string channelName);
    }
}
