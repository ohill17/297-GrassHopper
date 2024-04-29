using Microsoft.AspNetCore.Mvc;
using GrassHopper.Models;
using GrassHopper.Data;
using GrassHopper.Data.Repositories;
using Newtonsoft.Json;
using System.Web;

namespace GrassHopper.Controllers
{
    public class ReviewController : Controller
    {
        private readonly AppDbContext _context; // Corrected field name
        private readonly IReviewRepository _reviewRepo;

        public ReviewController(AppDbContext context, IReviewRepository reviewRepo)
        {
            _context = context;
            _reviewRepo = reviewRepo;
        }
        public IActionResult ReviewPost()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Index(string reviewsFromFacebook) 
        {
            var reviews = _reviewRepo.GetAllReviews();
            if (reviewsFromFacebook != null && reviewsFromFacebook.Length > 0)
            {
                Review r = FacebookReviews.NewFromFacebook(HttpUtility.UrlDecode(reviewsFromFacebook));
                reviews.Add(r);
            }

            return View(reviews); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitReview(Review review)
        {
            if (ModelState.IsValid)
            {
                
                _context.Reviews.Add(review);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Review", "ReviewPost");
        }

    }
}