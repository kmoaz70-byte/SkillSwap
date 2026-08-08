using DataAccess.Repositories.Implementations;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IUnitofWork
    {
        public ISwapRequestRepository SwapRequest { get; }
        public ISkillRepository Skill { get; }
        IRatingRepository Rating { get; }
        IMessageRepository Message { get; }
        IUserSkillRepository UserSkill { get; }
        INotificationRepository Notification { get; }
        void Save();
        Task SaveAsync();
    }
}
