using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Models;
using Models.ViewModels;

namespace SkillSwap.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        private ProfileEditViewModel BuildEditViewModel(ApplicationUser user)
        {
            return new ProfileEditViewModel
            {
                FullName = user.FullName,
                Bio = user.Bio,
                Location = user.Location,
                Email = user.Email,
                ProfilePicturePath = user.ProfilePicture
            };
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var model = BuildEditViewModel(user);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var model = BuildEditViewModel(user);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProfileEditViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                model.Email = user.Email;
                model.ProfilePicturePath = user.ProfilePicture;
                return View(model);
            }

            user.FullName = model.FullName;
            user.Bio = model.Bio;
            user.Location = model.Location;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Profile updated successfully.";
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            model.Email = user.Email;
            model.ProfilePicturePath = user.ProfilePicture;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new PublicProfileViewModel
            {
                UserId = user.Id,
                FullName = user.FullName,
                Bio = user.Bio,
                Location = user.Location,
                ProfilePicturePath = user.ProfilePicture
            };

            return View(model);
        }
    }
}
