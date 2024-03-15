using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using GH.Models;
using GH.Data;
using GH.Data.Repositories;


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
            var reviews = rrepository.GetReviews();
            return View(reviews);
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
            if (ModelState.IsValid)
            {
                review.Date = DateTime.Now;
                if (review.ReviewId == 0)
                    await rrepository.StoreReviewsAsync(review);
                else
                    rrepository.UpdateReviews(review);
                return RedirectToAction("Index", "Reviews");
            }
            else
            {
                ViewBag.Action = (review.ReviewId == 0) ? "Add" : "Edit";
                return View(review);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int reviewId)
        {
            var review = await rrepository.GetReviewsByIdAsync(reviewId);
            return View(review);
        }

        [HttpPost]
        public IActionResult Delete(ReviewModel model)
        {
            rrepository.DeleteReviews(model.ReviewId);
            return RedirectToAction("Index", "Reviews");
        }
    }
}
