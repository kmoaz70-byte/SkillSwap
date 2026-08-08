using Businesslayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.ViewModels;
using System.Security.Claims;

[Authorize]
public class RatingController : Controller
{
    private readonly IRatingService _ratingService;

    public RatingController(IRatingService ratingService)
    {
        _ratingService = ratingService;
    }

    [HttpGet]
    public IActionResult Rate(int swapRequestId)
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var swapRequest = _ratingService.GetSwapRequestById(swapRequestId);
        if (swapRequest == null)
        {
            TempData["error"] = "Swap request not found";
            return RedirectToAction("Received", "Swap");
        }

        if (!_ratingService.CanUserRateSwap(swapRequest, userId))
        {
            TempData["error"] = "You cannot rate this swap";
            return RedirectToAction("Received", "Swap");
        }

        string otherUserId = _ratingService.GetOtherPartyId(swapRequest, userId);
        var otherUser = otherUserId == swapRequest.SenderId ? swapRequest.Sender : swapRequest.Receiver;

        var model = new RatingViewModel
        {
            SwapRequestId = swapRequest.Id,
            RatedUserId = otherUserId,
            RatedUserName = otherUser.FullName
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Rate(RatingViewModel model)
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var swapRequest = _ratingService.GetSwapRequestById(model.SwapRequestId);
        if (swapRequest == null || !_ratingService.CanUserRateSwap(swapRequest, userId))
        {
            TempData["error"] = "You cannot submit this rating";
            return RedirectToAction("Received", "Swap");
        }

        if (model.Stars < 1 || model.Stars > 5)
        {
            TempData["error"] = "Please select a valid star rating";
            return View(model);
        }

        var rating = new Rating
        {
            SwapRequestId = model.SwapRequestId,
            RaterId = userId,
            RatedUserId = model.RatedUserId,
            Stars = model.Stars,
            Comment = model.Comment
        };

        _ratingService.SubmitRating(rating);
        TempData["success"] = "Rating submitted successfully";
        return RedirectToAction("Received", "Swap");
    }

    [AllowAnonymous]
    public IActionResult UserRatings(string userId)
    {
        var ratings = _ratingService.GetRatingsForUser(userId);
        ViewBag.AverageRating = _ratingService.GetAverageRating(userId);

        return View(ratings);
    }
}