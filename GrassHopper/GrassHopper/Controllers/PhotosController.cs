//using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using GrassHopper.Data;
using GrassHopper.Data.Repositories;
using GrassHopper.Models;

namespace GrassHopper.Controllers
{
    public class PhotosController : Controller
    {
        private readonly IPhotosRepository prepository;
        private readonly AppDbContext context;
        //private readonly UserManager<AppUserModel> userManager;

        public PhotosController(IPhotosRepository p, AppDbContext c /*UserManager<AppUserModel> u*/)
        {
            prepository = p;
            context = c;
            //userManager = u;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add(int id)
        {
            ViewBag.Action = "Add";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(PhotoUploadModel model)
        {
            if (model.file.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await model.file.CopyToAsync(stream);
                    model.Photo = stream.ToArray();
                }
            }

            if (model.Photo != null)
            {
            }

            return RedirectToAction("Index", "Photos");
        }

        
        [HttpGet]
        public async Task<IActionResult> Delete(int photoId)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(PhotoModel model)
        {
            return RedirectToAction("Index", "Photos");
        }
        
    }
}
