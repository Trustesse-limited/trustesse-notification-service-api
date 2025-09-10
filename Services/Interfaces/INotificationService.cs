
namespace Ivoluntia.BackgroudServices.Services.Interfaces
{
    public interface INotificationService
    {
        Task<List<Notification>> GetUnsentEmailsAsync();
        void UpdateNotificationStatuses(IEnumerable<Notification> notifications);
    }
}
