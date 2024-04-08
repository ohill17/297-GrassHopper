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
        /*
        public IActionResult Index()
        {
            var photos = prepository.GetPhotos();
            return View(photos);
        }

        [HttpGet]
        public IActionResult Add(int id)
        {
            ViewBag.Action = "Add";
            var photo = prepository.GetPhotosByIdAsync(id).Result;
            return View(photo);
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
                await prepository.StorePhotosAsync(new PhotoModel
                {
                    PhotoName = model.PhotoName,
                    PhotoDescription = model.PhotoDescription,
                    Photo = model.Photo
                });
            }

            return RedirectToAction("Index", "Photos");
        }

        
        [HttpGet]
        public async Task<IActionResult> Delete(int photoId)
        {
            var photo = await prepository.GetPhotosByIdAsync(photoId);
            return View(photo);
        }

        [HttpPost]
        public IActionResult Delete(PhotoModel model)
        {
            prepository.DeletePhotos(model.PhotoId);
            return RedirectToAction("Index", "Photos");
        }
        */
        
    }
}
