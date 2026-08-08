using DataAccess.Data;
using DataAccess.Repositories.Implementations;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Enums;
using Models.Models;

public class SkillRepository : GenericRepository<Skill>, ISkillRepository
{
    private readonly ApplicationDbContext _db;

    public SkillRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public IEnumerable<Skill> GetApprovedSkills(string? includeProperties = null)
    {
        IQueryable<Skill> query = _db.Skills.Where(s => s.ApprovalStatus == SkillApprovalStatus.Approved);

        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var prop in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(prop.Trim());
            }
        }

        return query.ToList();
    }

    public IEnumerable<Skill> GetPendingSkills(string? includeProperties = null)
    {
        IQueryable<Skill> query = _db.Skills.Where(s => s.ApprovalStatus == SkillApprovalStatus.Pending);

        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var prop in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(prop.Trim());
            }
        }

        return query.ToList();
    }

    public void Update(Skill skill)
    {
        var skillFromDb = _db.Skills.FirstOrDefault(s => s.Id == skill.Id);
        if (skillFromDb != null)
        {
            skillFromDb.Name = skill.Name;
            skillFromDb.Category = skill.Category;
            skillFromDb.ApprovalStatus = skill.ApprovalStatus;
        }
    }
}