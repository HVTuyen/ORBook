using ORBook.Models;

namespace ORBook.Service.Notifications
{
    public interface INotificationService
    {
        Task<Notification> CreateAsync(Notification notification);
        Task NotifyUsersAsync(Volumn volumn);
        Task<List<Notification>> GetAllAsync(int userId);
        Task<int> GetCountUnreadNotificationsAsync(int userId);
        Task<Notification> Read(int Id);
    }
}
