using Models.Enums;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IUserSkillRepository:IGenericRepository<UserSkill>
    {
        IEnumerable<UserSkill> GetUserSkills(string userId, SkillType? skillType = null, string? includeProperties = null);
        void Update(UserSkill userSkill);

        PagedResult<UserSkill> Search(
    string currentUserId,
    string? searchTerm,
    SkillCategory? category,
    SkillType? type,
    ProficiencyLevel? proficiency,
    int pageNumber,
    int pageSize);
    }
}
