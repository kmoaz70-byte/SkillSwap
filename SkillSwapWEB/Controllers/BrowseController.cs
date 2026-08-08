using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using System.Security.Claims;

[Authorize]
public class BrowseController : Controller
{
    private readonly IUserSkillService _userSkillService;
    private const int PageSize = 9;

    public BrowseController(IUserSkillService userSkillService)
    {
        _userSkillService = userSkillService;
    }

    public IActionResult Index(BrowseFilterViewModel filter)
    {
        string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int pageNumber = filter.Page < 1 ? 1 : filter.Page;

        PagedResult<UserSkill> pagedResult = _userSkillService.SearchSkills(
            currentUserId,
            filter.SearchTerm,
            filter.Category,
            filter.Type,
            filter.Proficiency,
            pageNumber,
            PageSize);

        var viewModel = new BrowseIndexViewModel
        {
            Filter = filter,
            Results = pagedResult.Items.Select(us => new BrowseResultViewModel
            {
                UserSkillId = us.Id,
                SkillName = us.Skill.Name,
                Category = us.Skill.Category,
                Type = us.SkillType,
                Proficiency = us.ProficiencyLevel,
                AdditionalInfo = us.AdditionalInfo,
                UserId = us.UserId,
                UserFullName = us.User.FullName,
                UserProfilePicturePath = us.User.ProfilePicture,
                UserLocation = us.User.Location
            }).ToList(),
            TotalCount = pagedResult.TotalCount,
            PageNumber = pagedResult.PageNumber,
            PageSize = pagedResult.PageSize,
            TotalPages = pagedResult.TotalPages
        };

        return View(viewModel);
    }
}