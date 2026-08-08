using Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{

    public class SwapRequest
    {
        
        public int Id { get; set; }
        [Required]
        public string SenderId { get; set; } = string.Empty;
        public string ReceiverId { get; set; } = string.Empty;
        public int SenderSkillId { get; set; }
        public int ReceiverSkillId { get; set; }
        public string? Message { get; set; }
        public SwapStatus Status { get; set; } = SwapStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? RespondedAt { get; set; }

        // Navigation Properties
        public ApplicationUser Sender { get; set; } = null!;
        public ApplicationUser Receiver { get; set; } = null!;
        //public Skill OfferedSkill { get; set; } = null!;
        //public Skill RequestedSkill { get; set; } = null!;
        public UserSkill SenderSkill { get; set; } = null!;
        public UserSkill ReceiverSkill { get; set; } = null!;
    }
}
