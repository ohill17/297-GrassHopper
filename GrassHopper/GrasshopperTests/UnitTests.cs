using Xunit;
using GrassHopper.Controllers;
using GrassHopper.Data.Repositories;
using GrassHopper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GrasshopperTests
{
    public class UnitTests
    {

        [Fact]
        public void TestUploadReview()
        {
            var rRepo = new TestReviewRepository();
            var tRepo = new TestTokenRepository();
            var appSettings = new AppSettings();
            var revCont = new ReviewControllerTests(rRepo, tRepo, appSettings);
            string testReviewerName = "John Review";

            Review review = new() { ReviewerName = testReviewerName, ReviewBody = "Looks good to me!", ReviewRating = 5 };

            Assert.True(revCont.SubmitReview(review).Result is ActionResult);

            Assert.True(rRepo.GetAllReviews().Result.Any());
            Assert.True(rRepo.GetReview(0).Result.ReviewerName == testReviewerName);
        }

        [Fact]
        public void TestGetReview()
        {
            var rRepo = new TestReviewRepository();
            string testReviewerName = "John Review";
            rRepo.AddReview(new Review() { ReviewerName = testReviewerName }).Wait();
            Review rev = rRepo.GetReview(0).Result;
            Assert.True(rev.ReviewerName == testReviewerName);
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

        [Fact]
        public async Task TestGetFacebookReviews()
        {
            var rRepo = new TestReviewRepository();
            var tRepo = new TestTokenRepository();
            var appSettings = new AppSettings();
            var revCont = new ReviewControllerTests(rRepo, tRepo, appSettings);

            var result = await revCont.Index("[{\"created_time\":\"2024-04-10T22:09:46+0000\",\"review_text\":\"test\"}]", null, null, null);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult.Model);
            Assert.Equal(viewResult.Model.Reviews.Count, 0);
            //Assert.True(viewResult.Model.Reviews.Count > 0);
        }
    }
}