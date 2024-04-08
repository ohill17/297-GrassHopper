using Microsoft.AspNetCore.Mvc;
using GrassHopper.Models;
using GrassHopper.Data;

namespace GrassHopper.Controllers
{
    public class ReviewController : Controller
    {
        private readonly AppDbContext _context; // Corrected field name

        public ReviewController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult ReviewPost()
        {
            return View();
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public IActionResult Review() 
        { 
            _context.
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitReview(Review review)
        {
            if (ModelState.IsValid)
            {
                
                _context.Reviews.Add(review);
                _context.SaveChanges();
                return RedirectToAction("Review", "Review");
            }
            return View("Review", "ReviewPost");
        }

    }
}