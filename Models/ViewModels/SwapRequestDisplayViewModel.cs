using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class SwapRequestDisplayViewModel
    {
        public int Id { get; set; }
        public string OtherUserId { get; set; }
        public string OtherUserName { get; set; }
        public string MySkillName { get; set; }
        public string TheirSkillName { get; set; }
        public string? Message { get; set; }
        public SwapStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? RespondedAt { get; set; }
    }
}
