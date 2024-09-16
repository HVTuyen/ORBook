using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ORBook.Data;
using ORBook.Hubs;
using ORBook.Models;

namespace ORBook.Service.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly ORBookContext _orbookcontext;
        IHubContext<NotificationHub> _hubContext;
        public NotificationService(ORBookContext orbookcontext, IHubContext<NotificationHub> hubContext)
        {
            _orbookcontext = orbookcontext;
            _hubContext = hubContext;
        }
        public async Task<Notification> CreateAsync(Notification notification)
        {
            _orbookcontext.Add(notification);
            await _orbookcontext.SaveChangesAsync();
            return notification;

        }

        public async Task<List<Notification>> GetAllAsync(int userId)
        {
            return await _orbookcontext.Notification
                            .Include(n => n.Volumn.Book)
                            .Where(n => n.UserId == userId)
                            .OrderByDescending(n => n.CreatedTime)
                            .ToListAsync();
        }

        public async Task<int> GetCountUnreadNotificationsAsync(int userId)
        {
            return await _orbookcontext.Notification
                            .Include(n => n.Volumn.Book)
                            .Where(n => n.UserId == userId && n.IsRead == false)
                            .CountAsync();
        }

        public async Task NotifyUsersAsync(Volumn volumn)
        {
            var users = await _orbookcontext.BookUser
                            .Where(bu => bu.BookId == volumn.BookId)
                            .Select(bu => bu.User)
                            .ToListAsync();
            var book = await _orbookcontext.Book.FirstOrDefaultAsync(b => b.Id == volumn.BookId);
            foreach (var user in users)
            {
                var notification = new Notification
                {
                    UserId = user.Id,
                    VolumnId = volumn.Id,
                    CreatedTime = DateTime.Now,
                    IsRead = false
                };

                await CreateAsync(notification);
                await _hubContext.Clients.User(user.Id.ToString())
                    .SendAsync("ReceiveNotification", $"Sách {book.Name} vừa có tập mới!");
            }
        }

        public async Task<Notification> Read(int Id)
        {
            var notification = await _orbookcontext.Notification.Include(n => n.Volumn).FirstOrDefaultAsync(n => n.Id == Id);
            if (notification == null)
            {
                return null;
            }
            notification.IsRead = true;
            await _orbookcontext.SaveChangesAsync();
            return notification;
        }
    }
}
