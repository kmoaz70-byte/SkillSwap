using DataAccess.Data;
using DataAccess.Repositories.Implementations;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Enums;
using Models.Models;

public class UserSkillRepository : GenericRepository<UserSkill>, IUserSkillRepository
{
    private readonly ApplicationDbContext _db;

    public UserSkillRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public IEnumerable<UserSkill> GetUserSkills(string userId, SkillType? skillType = null, string? includeProperties = null)
    {
        IQueryable<UserSkill> query = _db.UserSkills.Where(us => us.UserId == userId);

        if (skillType.HasValue)
        {
            query = query.Where(us => us.SkillType == skillType.Value);
        }

        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var prop in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(prop.Trim());
            }
        }

        return query.ToList();
    }

    public void Update(UserSkill userSkill)
    {
        var fromDb = _db.UserSkills.FirstOrDefault(us => us.Id == userSkill.Id);
        if (fromDb != null)
        {
            fromDb.SkillType = userSkill.SkillType;
            fromDb.ProficiencyLevel = userSkill.ProficiencyLevel;
            
        }
    }
    public PagedResult<UserSkill> Search(
    string currentUserId,
    string? searchTerm,
    SkillCategory? category,
    SkillType? type,
    ProficiencyLevel? proficiency,
    int pageNumber,
    int pageSize)
    {
        IQueryable<UserSkill> query = _db.UserSkills
            .Include(u => u.Skill)
            .Include(u => u.User)
            .AsNoTracking()
            .Where(u => u.UserId != currentUserId
                     && u.Skill.ApprovalStatus == SkillApprovalStatus.Approved
                     && u.User.IsActive);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            string term = searchTerm.ToLower();
            query = query.Where(u => u.Skill.Name.ToLower().Contains(term));
        }

        if (category.HasValue)
            query = query.Where(u => u.Skill.Category == category.Value);

        if (type.HasValue)
            query = query.Where(u => u.SkillType == type.Value);

        if (proficiency.HasValue)
            query = query.Where(u => u.ProficiencyLevel == proficiency.Value);

        int totalCount = query.Count();

        List<UserSkill> items = query
            .OrderBy(u => u.Skill.Name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PagedResult<UserSkill>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }
}