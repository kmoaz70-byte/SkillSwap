using Businesslayer.Services.Interfaces;
using DataAccess.Repositories;
using Models.Enums;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businesslayer.Services.Implementations
{
    public class RatingService : IRatingService
    {
        private readonly IUnitofWork _unitOfWork;

        public RatingService(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public SwapRequest? GetSwapRequestById(int swapRequestId)
        {
            return _unitOfWork.SwapRequest.GetOne(s => s.Id == swapRequestId, "Sender,Receiver,SenderSkill,ReceiverSkill");
        }

        public bool HasUserAlreadyRated(int swapRequestId, string raterId)
        {
            var existing = _unitOfWork.Rating.GetOne(r => r.SwapRequestId == swapRequestId && r.RaterId == raterId);
            return existing != null;
        }

        public bool CanUserRateSwap(SwapRequest swapRequest, string userId)
        {
            if (swapRequest.Status != SwapStatus.Completed)
                return false;

            bool isParticipant = swapRequest.SenderId == userId || swapRequest.ReceiverId == userId;
            if (!isParticipant)
                return false;

            if (HasUserAlreadyRated(swapRequest.Id, userId))
                return false;

            return true;
        }

        public string GetOtherPartyId(SwapRequest swapRequest, string currentUserId)
        {
            return swapRequest.SenderId == currentUserId ? swapRequest.ReceiverId : swapRequest.SenderId;
        }

        public void SubmitRating(Rating rating)
        {
            _unitOfWork.Rating.Add(rating);
            _unitOfWork.Save();
        }

        public IEnumerable<Rating> GetRatingsForUser(string userId)
        {
            return _unitOfWork.Rating.GetAll("Rater,SwapRequest")
                .Where(r => r.RatedUserId == userId)
                .OrderByDescending(r => r.CreatedAt);
        }

        public double GetAverageRating(string userId)
        {
            var ratings = GetRatingsForUser(userId).ToList();
            return ratings.Any() ? ratings.Average(r => r.Stars) : 0;
        }
    }
}
