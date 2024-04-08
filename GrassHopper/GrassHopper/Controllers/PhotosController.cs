//using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using GrassHopper.Data;
using GrassHopper.Models;
using GrassHopper.Data.Repositories;
using System.IO;
using System.Web;

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

        }

        [HttpGet]
        public IActionResult AddPhoto()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPhoto(PhotoUploadVM model)
        {
            if (model.File != null)
            {
                //This should result in a close to unique string to use as a unique file name
                string imageCode = model.PhotoName + '_' + model.File.Length.GetHashCode().ToString() 
                    + model.File.Headers.GetHashCode().ToString() + model.File.Name.GetHashCode().ToString() + Path.GetExtension(model.File.FileName);


            }

            return RedirectToAction("Index", "Photos");
        }

        

        //These are currently non-functional for a number of reasons

        [HttpGet]
        public async Task<IActionResult> Delete(int photoId)
        {

        }

        [HttpPost]
        public IActionResult Delete(PhotoModel model)
        {

            return RedirectToAction("Index", "Photos");
        }
        */
        
    }
}
