using Xunit;
using GrassHopper.Controllers;
using GrassHopper.Data.Repositories;
using GrassHopper.Models;
using Microsoft.AspNetCore.Mvc;

namespace GrasshopperTests
{
    public class UnitTests
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
        [Fact]
        public async Task SubmitQuote_ValidModel_RedirectsToQuoteSubmitted()
        {
            var emailSender = new EmailSender();
            var controller = new HomeControllerTests(emailSender);
            var quote = new Quote
            {
                FName = "John",
                LName = "Doe",
                PhoneNumber = "1234567890",
                Email = "john.doe@example.com",
                Body = "Test message"
            };
            //Note these actually send emails to me so test sparingly. -Orion
            var result = await controller.SubmitQuote(quote);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("QuoteSubmitted", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task SubmitQuote_InvalidModel_ReturnsViewWithError()
        {
            var emailSender = new EmailSender();
            var controller = new HomeControllerTests(emailSender);
            var quote = new Quote();

            controller.ModelState.AddModelError("Email", "Email is required.");

            var result = await controller.SubmitQuote(quote);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
            Assert.False(viewResult.ViewData.ModelState.IsValid);
        }
    }
}