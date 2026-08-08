using DataAccess.Data;
using DataAccess.Repositories.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Implementations
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        private readonly ApplicationDbContext _db;

        public NotificationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Notification notification)
        {
            var objFromDb = _db.Notifications.FirstOrDefault(n => n.Id == notification.Id);
            if (objFromDb != null)
            {
                objFromDb.IsRead = notification.IsRead;
                objFromDb.Message = notification.Message;
                objFromDb.Link = notification.Link;
            }
        }
    }
}
