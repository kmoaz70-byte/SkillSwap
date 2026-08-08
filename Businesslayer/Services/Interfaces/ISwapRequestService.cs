using Models.Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businesslayer.Services.Interfaces
{
   

public interface ISwapRequestService
    {
        void CreateRequest(string senderId, int senderSkillId, string receiverId, int receiverSkillId, string? message);
        IEnumerable<SwapRequestDisplayViewModel> GetSentRequests(string userId);
        IEnumerable<SwapRequestDisplayViewModel> GetReceivedRequests(string userId);
        void AcceptRequest(int requestId, string currentUserId);
        void RejectRequest(int requestId, string currentUserId);
        void CancelRequest(int requestId, string currentUserId);
        UserSkill? GetUserSkillById(int userSkillId);
        IEnumerable<UserSkill> GetOfferingSkills(string userId);
        void MarkAsCompleted(int swapRequestId, string currentUserId);
        SwapRequest GetRequestById(int id);
    }
}

