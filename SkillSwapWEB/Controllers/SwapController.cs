using Businesslayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Models;
using Models.ViewModels;
using System.Security.Claims;

namespace SkillSwapWEB.Controllers
{

    [Authorize]
    public class SwapController : Controller
    {
        private readonly ISwapRequestService _swapRequestService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INotificationService _notificationService;

        public SwapController(ISwapRequestService swapRequestService, UserManager<ApplicationUser> userManager, INotificationService notificationService)
        {
            _swapRequestService = swapRequestService;
            _userManager = userManager;
            _notificationService = notificationService;
        }

        [HttpGet]
        public IActionResult Request(string receiverId, int receiverSkillId)
        {
            var currentUserId = _userManager.GetUserId(User);
            var receiverSkill = _swapRequestService.GetUserSkillById(receiverSkillId);

            if (receiverSkill == null)
            {
                return NotFound();
            }

            var myOfferingSkills = _swapRequestService.GetOfferingSkills(currentUserId);

            var model = new SwapRequestCreateViewModel
            {
                ReceiverId = receiverId,
                ReceiverName = receiverSkill.User.FullName,
                ReceiverSkillId = receiverSkill.Id,
                ReceiverSkillName = receiverSkill.Skill.Name,
                MyOfferingSkills = myOfferingSkills.Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = $"{u.Skill.Name} ({u.ProficiencyLevel})"
                })
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Request(SwapRequestCreateViewModel model)
        {
            var currentUserId = _userManager.GetUserId(User);
            var receiverSkill = _swapRequestService.GetUserSkillById(model.ReceiverSkillId);

            if (receiverSkill == null)
            {
                return NotFound();
            }

            try
            {
                _swapRequestService.CreateRequest(currentUserId, model.RequesterSkillId, receiverSkill.UserId, model.ReceiverSkillId, model.Message);

                await _notificationService.CreateNotificationAsync(
                    receiverSkill.UserId,
                    "You received a new swap request.",
                    "/Swap/Received"
                );

                TempData["Success"] = "Swap request sent successfully.";
                return RedirectToAction("Sent");
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Request", new { receiverId = receiverSkill.UserId, receiverSkillId = model.ReceiverSkillId });
            }
        }

        [HttpGet]
        public IActionResult Sent()
        {
            var currentUserId = _userManager.GetUserId(User);
            var requests = _swapRequestService.GetSentRequests(currentUserId);
            return View(requests);
        }

        [HttpGet]
        public IActionResult Received()
        {
            var currentUserId = _userManager.GetUserId(User);
            var requests = _swapRequestService.GetReceivedRequests(currentUserId);
            return View(requests);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Accept(int id)
        {
            var currentUserId = _userManager.GetUserId(User);
            _swapRequestService.AcceptRequest(id, currentUserId);

            var swapRequest = _swapRequestService.GetRequestById(id);
            if (swapRequest != null)
            {
                await _notificationService.CreateNotificationAsync(
                    swapRequest.SenderId,
                    "Your swap request was accepted.",
                    "/Swap/Sent"
                );
            }

            TempData["Success"] = "Swap request accepted.";
            return RedirectToAction("Received");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int id)
        {
            var currentUserId = _userManager.GetUserId(User);
            _swapRequestService.RejectRequest(id, currentUserId);

            var swapRequest = _swapRequestService.GetRequestById(id);
            if (swapRequest != null)
            {
                await _notificationService.CreateNotificationAsync(
                    swapRequest.SenderId,
                    "Your swap request was rejected.",
                    "/Swap/Sent"
                );
            }

            TempData["Success"] = "Swap request rejected.";
            return RedirectToAction("Received");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cancel(int id)
        {
            var currentUserId = _userManager.GetUserId(User);
            _swapRequestService.CancelRequest(id, currentUserId);

            TempData["Success"] = "Swap request cancelled.";
            return RedirectToAction("Sent");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkComplete(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            try
            {
                _swapRequestService.MarkAsCompleted(id, userId);

                var swapRequest = _swapRequestService.GetRequestById(id);
                if (swapRequest != null)
                {
                    var otherUserId = swapRequest.SenderId == userId ? swapRequest.ReceiverId : swapRequest.SenderId;
                    await _notificationService.CreateNotificationAsync(
                        otherUserId,
                        "A swap was marked as completed. You can now leave a rating.",
                        "/Swap/Received"
                    );
                }

                TempData["success"] = "Swap marked as completed. You can now rate the other user.";
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            return RedirectToAction("Received");
        }
    }
}