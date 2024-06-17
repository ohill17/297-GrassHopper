using Microsoft.AspNetCore.Mvc;
using GrassHopper.Models;
using GrassHopper.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using GrassHopper.Data;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using MessagePack.Resolvers;

namespace GrassHopper.Controllers
{
    public class ReviewControllerTests : Controller
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly ITokenRepository _tokenRepo;
        private readonly AppSettings _appSettings;

        public ReviewControllerTests(IReviewRepository reviewRepo, ITokenRepository tokenRepo, AppSettings appSettings)
        {
            _reviewRepo = reviewRepo;
            _tokenRepo = tokenRepo;
            _appSettings = appSettings;
        }

        public IActionResult ReviewPost()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Index(string reviewsFromFacebook, string longPAccessToken, string longUAccessToken, string updateLongUAccessToken)
        {
            ReviewsVM reviewsVM = new ReviewsVM();
            /*reviewsVM.FacebookAppId = _appSettings.FacebookAppId;
            reviewsVM.FacebookAppSecret = _appSettings.FacebookAppSecret;
            reviewsVM.FacebookRedirectUri = _appSettings.FacebookRedirectUri;*/

            //checking if a user is logged in
         /*   if (User.IsInRole("Admin"))
            {
                ViewData["IsAdmin"] = "true";
            } else
            {
                ViewData["IsAdmin"] = "false";
            }*/

            // Loading facebook reviews onto the page
            ViewData["IsLoaded"] = "false";
            var reviews = await _reviewRepo.GetAllReviews();
            if (!string.IsNullOrEmpty(reviewsFromFacebook))
            {
                Review r = FacebookReviews.NewFromFacebook(HttpUtility.UrlDecode(reviewsFromFacebook));
                reviews.Add(r);
                ViewData["IsLoaded"] = "true";
            }
            reviewsVM.Reviews = reviews;

            // Loading an access token into the database
            if (!string.IsNullOrEmpty(longPAccessToken))
            {
                Token t = new Token { 
                    TokenString = longPAccessToken,
                    TokenLength = "long",
                    TokenType = "page",
                    CreationTime = DateTime.Now
                };
                await _tokenRepo.AddToken(t);
            }
            if (!string.IsNullOrEmpty(longUAccessToken))
            {
                Token t = new Token
                {
                    TokenString = longUAccessToken,
                    TokenLength = "long",
                    TokenType = "user",
                    CreationTime = DateTime.Now
                };
                await _tokenRepo.AddToken(t);
            }

            var theTokens = await _tokenRepo.GetAllTokens();

            // Putting the existing valid access tokens in the ViewBag, but if there are no valid tokens for a type then the value is "none"
            ViewBag.spaToken = "none";
            ViewBag.suaToken = "none";
            ViewBag.luaToken = "none";
            ViewBag.lpaToken = "none";
            
            foreach (Token t in theTokens)
            {
                DateTime invalidationDate = t.CreationTime.AddDays(60);
                if (t.CreationTime < invalidationDate)
                {
                    switch (t.TokenLength, t.TokenType)
                    {
                        case ("short", "page"):
                            ViewBag.spaToken = t.TokenString;
                            break;
                        case ("short", "user"):
                            ViewBag.suaToken = t.TokenString;
                            break;
                        case ("long", "page"):
                            ViewBag.lpaToken = t.TokenString;
                            break;
                        case ("long", "user"):

                            // Updating existing access tokens if neccesary 
                            if (updateLongUAccessToken == "true")
                            {
                                t.CreationTime = DateTime.Now;
                                await _tokenRepo.UpdateToken(t);
                            }
                            ViewBag.luaToken = t.TokenString;
                            break;
                    }
                } else
                {
                    //token is invalid, needs deleted
                    await _tokenRepo.DeleteToken(t.TokenID);
                }
            }

            return View(reviewsVM);
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
