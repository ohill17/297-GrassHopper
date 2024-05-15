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

        public HomeController(ILogger<HomeController> logger, AppDbContext c, IPhotoRepository p)
        {
            _logger = logger;
            context = c;
            prepository = p;
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
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}