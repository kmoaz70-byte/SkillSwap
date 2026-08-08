using Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class SuggestSkillViewModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public SkillCategory Category { get; set; }
    }
}