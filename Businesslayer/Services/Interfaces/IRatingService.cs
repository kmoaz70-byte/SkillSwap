using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businesslayer.Services.Interfaces
{
    public interface IRatingService
    {
        SwapRequest? GetSwapRequestById(int swapRequestId);
        bool HasUserAlreadyRated(int swapRequestId, string raterId);
        bool CanUserRateSwap(SwapRequest swapRequest, string userId);
        string GetOtherPartyId(SwapRequest swapRequest, string currentUserId);
        void SubmitRating(Rating rating);
        IEnumerable<Rating> GetRatingsForUser(string userId);
        double GetAverageRating(string userId);
    }
}
