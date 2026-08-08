using Businesslayer.Services.Interfaces;
using DataAccess.Repositories;
using Models.Enums;
using Models.Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businesslayer.Services.Implementations
{
  

    public class SwapRequestService : ISwapRequestService
    {
        private readonly IUnitofWork _unitOfWork;

        public SwapRequestService(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateRequest(string senderId, int senderSkillId, string receiverId, int receiverSkillId, string? message)
        {
            if (senderId == receiverId)
            {
                throw new InvalidOperationException("You cannot send a swap request to yourself.");
            }

            bool duplicateExists = _unitOfWork.SwapRequest
                .GetAll()
                .Any(s => s.SenderId == senderId
                       && s.ReceiverSkillId == receiverSkillId
                       && s.Status == SwapStatus.Pending);

            if (duplicateExists)
            {
                throw new InvalidOperationException("You already have a pending request for this skill.");
            }

            var swapRequest = new SwapRequest
            {
                SenderId = senderId,
                SenderSkillId = senderSkillId,
                ReceiverId = receiverId,
                ReceiverSkillId = receiverSkillId,
                Message = message,
                Status = SwapStatus.Pending,
                CreatedAt = DateTime.Now
            };

            _unitOfWork.SwapRequest.Add(swapRequest);
            _unitOfWork.Save();
        }

        public IEnumerable<SwapRequestDisplayViewModel> GetSentRequests(string userId)
        {
            var requests = _unitOfWork.SwapRequest
                .GetAll("Receiver,SenderSkill.Skill,ReceiverSkill.Skill")
                .Where(s => s.SenderId == userId)
                .OrderByDescending(s => s.CreatedAt);

            return requests.Select(s => MapToViewModel(s, isSentByMe: true));
        }

        public IEnumerable<SwapRequestDisplayViewModel> GetReceivedRequests(string userId)
        {
            var requests = _unitOfWork.SwapRequest
                .GetAll("Sender,SenderSkill.Skill,ReceiverSkill.Skill")
                .Where(s => s.ReceiverId == userId)
                .OrderByDescending(s => s.CreatedAt);

            return requests.Select(s => MapToViewModel(s, isSentByMe: false));
        }

        public void AcceptRequest(int requestId, string currentUserId)
        {
            var request = _unitOfWork.SwapRequest.GetOne(s => s.Id == requestId);
            if (request == null || request.ReceiverId != currentUserId)
            {
                throw new InvalidOperationException("Request not found or not authorized.");
            }

            request.Status = SwapStatus.Accepted;
            request.RespondedAt = DateTime.Now;
            _unitOfWork.SwapRequest.Update(request);
            _unitOfWork.Save();
        }

        public void RejectRequest(int requestId, string currentUserId)
        {
            var request = _unitOfWork.SwapRequest.GetOne(s => s.Id == requestId);
            if (request == null || request.ReceiverId != currentUserId)
            {
                throw new InvalidOperationException("Request not found or not authorized.");
            }

            request.Status = SwapStatus.Rejected;
            request.RespondedAt = DateTime.Now;
            _unitOfWork.SwapRequest.Update(request);
            _unitOfWork.Save();
        }

        public void CancelRequest(int requestId, string currentUserId)
        {
            var request = _unitOfWork.SwapRequest.GetOne(s => s.Id == requestId);
            if (request == null || request.SenderId != currentUserId)
            {
                throw new InvalidOperationException("Request not found or not authorized.");
            }

            request.Status = SwapStatus.Cancelled;
            request.RespondedAt = DateTime.Now;
            _unitOfWork.SwapRequest.Update(request);
            _unitOfWork.Save();
        }

        public UserSkill? GetUserSkillById(int userSkillId)
        {
            return _unitOfWork.UserSkill.GetOne(u => u.Id == userSkillId, "Skill,User");
        }

        public IEnumerable<UserSkill> GetOfferingSkills(string userId)
        {
            return _unitOfWork.UserSkill
                .GetAll("Skill")
                .Where(u => u.UserId == userId && u.SkillType == SkillType.Offering)
                .ToList();
        }

        private SwapRequestDisplayViewModel MapToViewModel(SwapRequest s, bool isSentByMe)
        {
            return new SwapRequestDisplayViewModel
            {
                Id = s.Id,
                OtherUserId = isSentByMe ? s.ReceiverId : s.SenderId,
                OtherUserName = isSentByMe ? s.Receiver.FullName : s.Sender.FullName,
                MySkillName = isSentByMe ? s.SenderSkill.Skill.Name : s.ReceiverSkill.Skill.Name,
                TheirSkillName = isSentByMe ? s.ReceiverSkill.Skill.Name : s.SenderSkill.Skill.Name,
                Message = s.Message,
                Status = s.Status,
                CreatedAt = s.CreatedAt,
                RespondedAt = s.RespondedAt
            };
        }
        public SwapRequest GetRequestById(int id)
        {
            return _unitOfWork.SwapRequest.GetOne(s => s.Id == id, "Sender,Receiver");
        }
        public void MarkAsCompleted(int swapRequestId, string currentUserId)
        {
            var swapRequest = _unitOfWork.SwapRequest.GetOne(s => s.Id == swapRequestId);

            if (swapRequest == null)
                throw new Exception("Swap request not found");

            bool isParticipant = swapRequest.SenderId == currentUserId || swapRequest.ReceiverId == currentUserId;
            if (!isParticipant)
                throw new UnauthorizedAccessException("You are not part of this swap");

            if (swapRequest.Status != SwapStatus.Accepted)
                throw new InvalidOperationException("Only accepted swaps can be marked as completed");

            swapRequest.Status = SwapStatus.Completed;
            _unitOfWork.SwapRequest.Update(swapRequest);
            _unitOfWork.Save();
        }
    }
}
