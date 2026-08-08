using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface ISkillRepository:IGenericRepository<Skill>
    {
        IEnumerable<Skill> GetApprovedSkills(string? includeProperties = null);
        IEnumerable<Skill> GetPendingSkills(string? includeProperties = null);
        public void Update(Skill skill);
    }
}
