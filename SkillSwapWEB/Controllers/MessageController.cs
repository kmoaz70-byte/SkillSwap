using DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace SkillSwapWEB.Controllers
{
   

    [Authorize]
    public class MessageController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public MessageController(IUnitofWork unitofWork, UserManager<ApplicationUser> userManager)
        {
            _unitofWork = unitofWork;
            _userManager = userManager;
        }

        public async Task<IActionResult> Chat(string otherUserId)
        {
            string currentUserId = _userManager.GetUserId(User)!;

            var messages = _unitofWork.Message.GetAll(includeProperties: "Sender,Receiver")
                .Where(m => (m.SenderId == currentUserId && m.ReceiverId == otherUserId) ||
                            (m.SenderId == otherUserId && m.ReceiverId == currentUserId))
                .OrderBy(m => m.SentAt)
                .ToList();

            var otherUser = await _userManager.FindByIdAsync(otherUserId);

            ViewBag.OtherUserId = otherUserId;
            ViewBag.OtherUserName = otherUser?.FullName ?? "Unknown User";
            ViewBag.CurrentUserId = currentUserId;

            return View(messages);
        }

        [HttpPost]
        public async Task<IActionResult> SaveMessage(string receiverId, string content)
        {
            string senderId = _userManager.GetUserId(User)!;

            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content,
                SentAt = DateTime.UtcNow,
                IsRead = false
            };

            _unitofWork.Message.Add(message);
            _unitofWork.Save();

            return Ok();
        }
    }
}
