using DataAccess.Data;
using DataAccess.Repositories.Implementations;
using DataAccess.Repositories.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UnitofWork : IUnitofWork
    {
        private readonly ApplicationDbContext _db;

        public ISwapRequestRepository SwapRequest { get; private set; }
        public IRatingRepository Rating { get; private set; }

        public ISkillRepository Skill { get; private set; }
        public IUserSkillRepository UserSkill { get; private set; }
        public IMessageRepository Message { get; private set; }
        public INotificationRepository Notification { get; }

        public UnitofWork(ApplicationDbContext db)
        {
            _db = db;
            SwapRequest = new SwapRequestRepository(db);
            Skill = new SkillRepository(db);
            Rating = new RatingRepository(_db);
            UserSkill = new UserSkillRepository(_db);
            Message = new MessageRepository(_db);
            Notification = new NotificationRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public Task SaveAsync() =>  _db.SaveChangesAsync();
    }
}
