using Models.Enums;
using Models.Models;
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class UserSkillViewModel
    {
        public int Id { get; set; }

        [Required]
        public int SkillId { get; set; }

        [Required]
        public SkillType SkillType { get; set; }

        [Required]
        public ProficiencyLevel ProficiencyLevel { get; set; }

        public string? AdditionalInfo { get; set; }

        // For populating the dropdown in the view
        public IEnumerable<Skill>? AvailableSkills { get; set; }
    }
}