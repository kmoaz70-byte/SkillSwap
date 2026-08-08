using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        //public string Category { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        public SkillCategory Category { get; set; }
        public SkillApprovalStatus ApprovalStatus { get; set; }
        public string? SuggestedByUserId { get; set; }
        public ApplicationUser? SuggestedByUser { get; set; }

        // Navigation Property
        public ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();
    }
}
