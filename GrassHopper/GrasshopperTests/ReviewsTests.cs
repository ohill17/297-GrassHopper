using Xunit;
using GrassHopper.Controllers;
using GrassHopper.Data.Repositories;
using GrassHopper.Models;
using Microsoft.AspNetCore.Mvc;

namespace GrasshopperTests
{
    public class ReviewsTests
    {
        [Fact]
        public void TestUploadReview()
        {
            var rRepo = new TestReviewRepository();
            var tRepo = new TestTokenRepository();
            var revCont = new ReviewController(rRepo, tRepo);
            string testReviewerName = "John Review";

            Review review = new() { ReviewerName = testReviewerName, ReviewBody = "Looks good to me!", ReviewRating = 5 };

            Assert.True(revCont.SubmitReview(review).Result is ActionResult);

            Assert.True(rRepo.GetAllReviews().Result.Any());
            Assert.True(rRepo.GetReview(0).Result.ReviewerName == testReviewerName);
        }
    }
}