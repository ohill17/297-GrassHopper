using Microsoft.AspNetCore.Mvc;
using GrassHopper.Models;
using GrassHopper.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using GrassHopper.Data;

namespace GrassHopper.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly ITokenRepository _tokenRepo;

        public ReviewController(IReviewRepository reviewRepo, ITokenRepository tokenRepo)
        {
            _reviewRepo = reviewRepo;
            _tokenRepo = tokenRepo;
        }

        public IActionResult ReviewPost()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Index(string reviewsFromFacebook, string longPAccessToken)
        {
            // Loading facebook reviews onto the page
            var reviews = await _reviewRepo.GetAllReviews();
            if (!string.IsNullOrEmpty(reviewsFromFacebook))
            {
                Review r = FacebookReviews.NewFromFacebook(HttpUtility.UrlDecode(reviewsFromFacebook));
                reviews.Add(r);
            }

            // Loading an access token into the database
            if (!string.IsNullOrEmpty(longPAccessToken))
            {
                Token t = new Token { TokenString = longPAccessToken };
                await _tokenRepo.AddToken(t);
            }

            // Sending the existing access token to the page
            var theTokens = await _tokenRepo.GetAllTokens();
            ViewBag.token = theTokens.Count > 0 ? theTokens[0].TokenString : string.Empty;

            return View(reviews);
        }

        [HttpGet]
        public IActionResult SubmitReview()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitReview(Review review)
        {
            if (ModelState.IsValid)
            {
                review.ReviewDate = DateTime.Now;
                await _reviewRepo.AddReview(review);
                return RedirectToAction("Index"); // Redirect to Index after successful submission
            }
            return View("ReviewPost", review); // Return to the form view if the model state is invalid
        }
    }
}
