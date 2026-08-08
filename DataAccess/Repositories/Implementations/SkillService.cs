using DataAccess.Repositories;
using Models;
using Models.Enums;
using Models.Models;

public class SkillService : ISkillService
{
    private readonly IUnitofWork _unitOfWork;

    public SkillService(IUnitofWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IEnumerable<Skill> GetApprovedSkills(SkillCategory? category = null)
    {
        var skills = _unitOfWork.Skill.GetApprovedSkills();

        if (category.HasValue)
        {
            skills = skills.Where(s => s.Category == category.Value);
        }

        return skills;
    }

    public Skill? GetSkillById(int id)
    {
        return _unitOfWork.Skill.GetOne(s => s.Id == id);
    }

    public void SuggestSkill(string name, SkillCategory category, string suggestedByUserId)
    {
        var skill = new Skill
        {
            Name = name,
            Category = category,
            ApprovalStatus = SkillApprovalStatus.Pending,
            SuggestedByUserId = suggestedByUserId
        };

        _unitOfWork.Skill.Add(skill);
        _unitOfWork.Save();
    }
}