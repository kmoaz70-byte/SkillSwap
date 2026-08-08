using Models;
using Models.Enums;
using Models.Models;

public interface ISkillService
{
    IEnumerable<Skill> GetApprovedSkills(SkillCategory? category = null);
    Skill? GetSkillById(int id);
    void SuggestSkill(string name, SkillCategory category, string suggestedByUserId);
}