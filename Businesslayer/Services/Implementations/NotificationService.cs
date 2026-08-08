using Businesslayer.Services.Interfaces;
using DataAccess.Repositories;
using Models.Models;

public class NotificationService : INotificationService
{
    private readonly IUnitofWork _unitOfWork;

    public NotificationService(IUnitofWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateNotificationAsync(string userId, string message, string? link = null)
    {
        var notification = new Notification
        {
            UserId = userId,
            Message = message,
            Link = link,
            IsRead = false,
            CreatedAt = DateTime.Now
        };

        _unitOfWork.Notification.Add(notification);
        await _unitOfWork.SaveAsync();
    }

    public List<Notification> GetUserNotifications(string userId)
    {
        return _unitOfWork.Notification
            .GetAll(includeProperties: null)
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .Take(10)
            .ToList();
    }

    public int GetUnreadCount(string userId)
    {
        return _unitOfWork.Notification
            .GetAll(includeProperties: null)
            .Count(n => n.UserId == userId && !n.IsRead);
    }

    public async Task MarkAsReadAsync(int notificationId)
    {
        var notification = _unitOfWork.Notification.GetOne(n => n.Id == notificationId);
        if (notification != null)
        {
            notification.IsRead = true;
            _unitOfWork.Notification.Update(notification);
            await _unitOfWork.SaveAsync();
        }
    }

    public async Task MarkAllAsReadAsync(string userId)
    {
        var notifications = _unitOfWork.Notification
            .GetAll(includeProperties: null)
            .Where(n => n.UserId == userId && !n.IsRead)
            .ToList();

        foreach (var n in notifications)
        {
            n.IsRead = true;
            _unitOfWork.Notification.Update(n);
        }

        await _unitOfWork.SaveAsync();
    }
}