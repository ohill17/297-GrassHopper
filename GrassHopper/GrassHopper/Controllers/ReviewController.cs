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
        private readonly ITokenRepository _tokenRepo;

        public ReviewController(AppDbContext context, IReviewRepository reviewRepo, ITokenRepository tokenRepo)
        {
            _context = context;
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
            var reviews = _reviewRepo.GetAllReviews();
            if (reviewsFromFacebook != null && reviewsFromFacebook.Length > 0)
            {
                Review r = FacebookReviews.NewFromFacebook(HttpUtility.UrlDecode(reviewsFromFacebook));
                reviews.Add(r);
            }

            // Loading an access token into the database
            if (longPAccessToken != null && longPAccessToken.Length > 0)
            {
                Token t = new Token();
                t.TokenString = longPAccessToken;
                await _tokenRepo.AddToken(t);
            }

            // Sending the existing access token to the page
            List<Token> theTokens = _tokenRepo.GetAllTokens();
            ViewBag.token = theTokens[0].TokenString;

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