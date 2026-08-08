using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{

    public class SwapRequestCreateViewModel
    {
        public string ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public int ReceiverSkillId { get; set; }
        public string ReceiverSkillName { get; set; }
        [Required(ErrorMessage = "Please select a skill to offer in return.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a skill to offer in return.")]
        public int RequesterSkillId { get; set; }
        public string? Message { get; set; }

        public IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>? MyOfferingSkills { get; set; }
    }
}
