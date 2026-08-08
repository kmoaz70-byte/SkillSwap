using Businesslayer.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.ViewModels;

public class NotificationViewComponent : ViewComponent
{
    private readonly INotificationService _notificationService;
    private readonly UserManager<ApplicationUser> _userManager;

    public NotificationViewComponent(INotificationService notificationService, UserManager<ApplicationUser> userManager)
    {
        _notificationService = notificationService;
        _userManager = userManager;
    }

    public IViewComponentResult Invoke()
    {
        var userId = _userManager.GetUserId(UserClaimsPrincipal);

        if (string.IsNullOrEmpty(userId))
        {
            return View(new NotificationViewModel());
        }

        var model = new NotificationViewModel
        {
            Notifications = _notificationService.GetUserNotifications(userId),
            UnreadCount = _notificationService.GetUnreadCount(userId)
        };

        return View(model);
    }
}