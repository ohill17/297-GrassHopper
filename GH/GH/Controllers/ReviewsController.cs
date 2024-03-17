using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using GH.Models;
using GH.Data;
using GH.Data.Repositories;
using GH.Data.Respositories;

namespace GH.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly AppDbContext context;
        private readonly IReviewsRepository rrepository;
        private readonly UserManager<AppUserModel> userManager;

        public ReviewsController(AppDbContext c, IReviewsRepository r, UserManager<AppUserModel> u)
        {
            context = c;
            rrepository = r;
            userManager = u;
        }

        public IActionResult Index()
        {
            var review = rrepository.GetReviews();
            return View(review);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new ReviewModel());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var review = rrepository.GetReviewsByIdAsync(id).Result;
            return View(review);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ReviewModel review)
        {

            if (!ModelState.IsValid)
                review.Date = DateTime.Now;
                return View(review);

            if (review.ReviewId == 0)
                await rrepository.StoreReviewsAsync(review);
            else
                rrepository.UpdateReviews(review);

            return RedirectToAction("Index", "Reviews");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var review = await rrepository.GetReviewsByIdAsync(id);
            return View(review);
        }

        [HttpPost]
        public IActionResult Delete(ReviewModel review)
        {
            rrepository.DeleteReviews(review.ReviewId);
            return RedirectToAction("Index", "Reviews");
        }
    }
}