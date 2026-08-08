using DataAccess.Repositories;
using Models;
using Models.Enums;
using Models.Models;

public class UserSkillService : IUserSkillService
{
    private readonly IUnitofWork _unitOfWork;

    public UserSkillService(IUnitofWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public PagedResult<UserSkill> SearchSkills(
    string currentUserId,
    string? searchTerm,
    SkillCategory? category,
    SkillType? type,
    ProficiencyLevel? proficiency,
    int pageNumber,
    int pageSize)
    {
        return _unitOfWork.UserSkill.Search(
            currentUserId, searchTerm, category, type, proficiency, pageNumber, pageSize);
    }
    public IEnumerable<UserSkill> GetUserSkills(string userId, SkillType? skillType = null)
    {
        SkillType? skillType1 = skillType;
        return _unitOfWork.UserSkill.GetUserSkills(userId, skillType1, "Skill");
    }

    public void AddUserSkill(UserSkill userSkill)
    {
        bool alreadyExists = _unitOfWork.UserSkill.GetUserSkills(userSkill.UserId)
            .Any(us => us.SkillId == userSkill.SkillId && us.SkillType == userSkill.SkillType);

        if (alreadyExists)
        {
            throw new InvalidOperationException("This skill is already in your list for this type.");
        }

        _unitOfWork.UserSkill.Add(userSkill);
        _unitOfWork.Save();
    }

    public void RemoveUserSkill(int userSkillId, string requestingUserId)
    {
        var userSkill = _unitOfWork.UserSkill.GetOne(us => us.Id == userSkillId);

        if (userSkill == null)
        {
            throw new KeyNotFoundException("Skill entry not found.");
        }

        if (userSkill.UserId != requestingUserId)
        {
            throw new UnauthorizedAccessException("You cannot remove another user's skill.");
        }

        _unitOfWork.UserSkill.Remove(userSkill);
        _unitOfWork.Save();
    }

   
}