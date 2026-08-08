using Models;
using Models.Enums;
using Models.Models;

public interface IUserSkillService
{
    IEnumerable<UserSkill> GetUserSkills(string userId, Models.Enums.SkillType? skillType = null);
    void AddUserSkill(UserSkill userSkill);
    void RemoveUserSkill(int userSkillId, string requestingUserId);
    PagedResult<UserSkill> SearchSkills(
    string currentUserId,
    string? searchTerm,
    SkillCategory? category,
    SkillType? type,
    ProficiencyLevel? proficiency,
    int pageNumber,
    int pageSize);
}