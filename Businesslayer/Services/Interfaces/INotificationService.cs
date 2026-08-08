using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businesslayer.Services.Interfaces
{
    public interface INotificationService
    {
        Task CreateNotificationAsync(string userId, string message, string? link = null);
        List<Notification> GetUserNotifications(string userId);
        int GetUnreadCount(string userId);
        Task MarkAsReadAsync(int notificationId);
        Task MarkAllAsReadAsync(string userId);
    }
}
