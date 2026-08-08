using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
   

    public class UserSkill
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int SkillId { get; set; }
        //public SkillType Type { get; set; }
        public string? AdditionalInfo { get; set; }

        public SkillType SkillType { get; set; }
        public ProficiencyLevel ProficiencyLevel { get; set; }

        // Navigation Properties
        public ApplicationUser User { get; set; } = null!;
        public Skill Skill { get; set; } = null!;
    }
}
