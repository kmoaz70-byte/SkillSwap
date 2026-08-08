using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class RatingViewModel
    {
        public int SwapRequestId { get; set; }
        public string RatedUserId { get; set; } = string.Empty;
        public string RatedUserName { get; set; } = string.Empty;
        public int Stars { get; set; }
        public string? Comment { get; set; }
    }
}
