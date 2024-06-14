using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Diagnostics;
using GrassHopper.Data;
using GrassHopper.Data.Repositories;
using GrassHopper.Models;

namespace GrassHopper.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext context;
        private readonly IPhotoRepository prepository;
        private readonly IEmailSender _emailSender;


        public HomeController(ILogger<HomeController> logger, AppDbContext c, IPhotoRepository p, IEmailSender emailSender)
        {
            _logger = logger;
            context = c;
            prepository = p;
            this._emailSender = emailSender;
        }

        public async Task<IActionResult> Index()
        {
            var photos = await prepository.GetAllPhotos(PhotoSize.Medium);
            return View(photos);
        }

        public async Task<IActionResult> AboutUs()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorVM { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        /*
            public async Task<IActionResult> SubmitQuote(Quote quote)
        {
            if (ModelState.IsValid)
            {
                // Concatenate all fields into a message
                var message = $"Name: {quote.FName} {quote.LName}\n" +
                              $"Phone Number: {quote.PhoneNumber}\n" +
                              $"Email: {quote.Email}\n" +
                              $"Message: {quote.Body}";

                // Set up your email sender details
                var sender = "grasshopperquotes@gmail.com";
                var subject = "A client is requesting a quote!";

                // Send email using SMTP
                await _emailSender.SendEmailAsync(sender, subject, message);

                // Assuming you want to redirect to a success page after successful submission
                return RedirectToAction("QuoteSubmitted");
            }

            // If model state is not valid, return the view to display validation errors
            return View();
        }
       
          */
        [HttpPost]
        public async Task<IActionResult> SubmitQuote(Quote quote)
        {
            if (ModelState.IsValid)
            {
                // Concatenate all fields into a message
                var message = $"Name: {quote.FName} {quote.LName}\n" +
                              $"Phone Number: {quote.PhoneNumber}\n" +
                              $"Email: {quote.Email}\n" +
                              $"Message: {quote.Body}";

                // Set up your email sender details
                var sender = "grasshopperquotes@gmail.com";
                var subject = quote.FName + " " + quote.LName + " " + "Is Requesting a Quote!";

                // Send email using SMTP
                await _emailSender.SendEmailAsync(sender, subject, message);

                // Assuming you want to redirect to a success page after successful submission
                return RedirectToAction("QuoteSubmitted");
            }

            // If model state is not valid, return the view to display validation errors
            return View();
        }
        public IActionResult RequestQuote()
        {
            return View();
        }
        public IActionResult QuoteSubmitted()
        {
            return View();
        }
    }
}
