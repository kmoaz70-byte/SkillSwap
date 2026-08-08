using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class NotificationViewModel
    {
        public List<Notification> Notifications { get; set; } = new List<Notification>();
        public int UnreadCount { get; set; } = 0;
    }
}
