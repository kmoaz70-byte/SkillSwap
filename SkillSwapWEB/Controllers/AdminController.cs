using DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Enums;
using Models.Models;
using Models.ViewModels;

namespace SkillSwapWEB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUnitofWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(IUnitofWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var skills = _unitOfWork.Skill.GetAll();
            var swapRequests = _unitOfWork.SwapRequest.GetAll();

            var model = new AdminDashboardViewModel
            {
                TotalUsers = _userManager.Users.Count(),
                TotalSkills = skills.Count(),
                PendingSkills = skills.Count(s => s.ApprovalStatus == SkillApprovalStatus.Pending),
                TotalSwapRequests = swapRequests.Count(),
                CompletedSwaps = swapRequests.Count(s => s.Status == SwapStatus.Completed)
            };

            return View(model);
        }
        public IActionResult Users()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }
        public async Task<IActionResult> DeactivateUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.IsActive = false;
            await _userManager.UpdateAsync(user);

            return RedirectToAction("Users");
        }

        public async Task<IActionResult> ActivateUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.IsActive = true;
            await _userManager.UpdateAsync(user);

            return RedirectToAction("Users");
        }
    }
}
