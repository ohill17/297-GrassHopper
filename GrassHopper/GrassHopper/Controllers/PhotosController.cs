//using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using GrassHopper.Data;
using GrassHopper.Models;
using GrassHopper.Data.Repositories;

namespace GrassHopper.Controllers
{
    public class PhotosController : Controller
    {
        private readonly IPhotoRepository pRepository;
        private readonly AppDbContext context;
        //private readonly UserManager<AppUserModel> userManager;

        public PhotosController(IPhotoRepository p, AppDbContext c /*UserManager<AppUserModel> u*/)
        {
            pRepository = p;
            context = c;
            //userManager = u;
        }

        public IActionResult Index()
        {
            var photos = pRepository.GetAllPhotos();
            return View(photos);
        }

        [HttpGet]
        public async Task<IActionResult> AddPhoto(int id)
        {
            ViewBag.Action = "Add";
            var photo = await pRepository.GetPhoto(id);
            return View(photo);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhoto(PhotoUploadVM model)
        {
            if (model.File != null)
            {
                //This should result in a close to unique string to use as a unique file name
                string imageCode = model.PhotoName + '_' + model.File.Length.GetHashCode().ToString() 
                    + model.File.Headers.GetHashCode().ToString() + model.File.Name.GetHashCode().ToString();
                PhotoModel photo = new()
                {
                    PhotoName = model.PhotoName,
                    PhotoCode = imageCode,
                    PhotoDescription = model.PhotoDescription
                };
                //MORE TO COME
            }

            return RedirectToAction("Index", "Photos");
        }

        
        [HttpGet]
        public async Task<IActionResult> Delete(int photoId)
        {
            var photo = 5;//await pRepository.GetPhotosByIdAsync(photoId);
            return View(photo);
        }

        [HttpPost]
        public IActionResult Delete(PhotoModel model)
        {
            //pRepository.DeletePhotos(model.PhotoId);
            return RedirectToAction("Index", "Photos");
        }
        
    }
}
