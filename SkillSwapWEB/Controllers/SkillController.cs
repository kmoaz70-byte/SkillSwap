using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Enums;
using Models.Models;
using Models.ViewModels;

namespace SkillSwap.Web.Controllers
{
    [Authorize]
    public class SkillController : Controller
    {
        private readonly IUserSkillService _userSkillService;
        private readonly ISkillService _skillService;
        private readonly UserManager<ApplicationUser> _userManager;

        public SkillController(IUserSkillService userSkillService, ISkillService skillService, UserManager<ApplicationUser> userManager)
        {
            _userSkillService = userSkillService;
            _skillService = skillService;
            _userManager = userManager;
        }

        // GET: My Skills page
        public IActionResult Index()
        {
            string userId = _userManager.GetUserId(User)!;

            var offered = _userSkillService.GetUserSkills(userId, SkillType.Offering);
            var wanted = _userSkillService.GetUserSkills(userId, SkillType.Seeking);

            ViewBag.OfferedSkills = offered;
            ViewBag.WantedSkills = wanted;

            return View();
        }

        // GET: Add skill form
        public IActionResult Add()
        {
            var model = new UserSkillViewModel
            {
                AvailableSkills = _skillService.GetApprovedSkills()
            };
            return View(model);
        }

        // POST: Add skill
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(UserSkillViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AvailableSkills = _skillService.GetApprovedSkills();
                return View(model);
            }

            string userId = _userManager.GetUserId(User)!;

            var userSkill = new UserSkill
            {
                UserId = userId,
                SkillId = model.SkillId,
                SkillType = model.SkillType,
                ProficiencyLevel = model.ProficiencyLevel,
                AdditionalInfo = model.AdditionalInfo
            };

            try
            {
                _userSkillService.AddUserSkill(userSkill);
                TempData["Success"] = "Skill added to your profile.";
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        // POST: Remove skill
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int id)
        {
            string userId = _userManager.GetUserId(User)!;

            try
            {
                _userSkillService.RemoveUserSkill(id, userId);
                TempData["Success"] = "Skill removed.";
            }
            catch (UnauthorizedAccessException)
            {
                TempData["Error"] = "You cannot remove this skill.";
            }
            catch (KeyNotFoundException)
            {
                TempData["Error"] = "Skill entry not found.";
            }

            return RedirectToAction("Index");
        }

        // GET: Suggest new skill form
        public IActionResult SuggestSkill()
        {
            return View(new SuggestSkillViewModel());
        }

        // POST: Suggest new skill
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuggestSkill(SuggestSkillViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userId = _userManager.GetUserId(User)!;
            _skillService.SuggestSkill(model.Name, model.Category, userId);

            TempData["Success"] = "Skill suggested — pending admin approval.";
            return RedirectToAction("Index");
        }
    }
}