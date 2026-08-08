using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalSkills { get; set; }
        public int PendingSkills { get; set; }
        public int TotalSwapRequests { get; set; }
        public int CompletedSwaps { get; set; }
    }
}
