using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface INotificationRepository : IGenericRepository<Notification>
    {
        void Update(Notification notification);
    }
}
