using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public string RaterId { get; set; } = string.Empty;
        public string RatedUserId { get; set; } = string.Empty;
        public int SwapRequestId { get; set; }
        public int Stars { get; set; } // 1 to 5
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public ApplicationUser Rater { get; set; } = null!;
        public ApplicationUser RatedUser { get; set; } = null!;
        public SwapRequest SwapRequest { get; set; } = null!;
    }
}
