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
			var photos = pRepository.GetAllPhotos();
			return View(photos);
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

				if (model.File.Length > 0)
				{
					//Confirms that the directory exists and creates it if not
					if(!Directory.Exists("./wwwroot/photos"))
						Directory.CreateDirectory("./wwwroot/photos");

					//Creates a file path to the 'photos' folder and append the target file name
					var filePath = Path.Combine("./wwwroot/photos/", imageCode);

					//Creates the target file and copies the data to it
					using var stream = System.IO.File.Create(filePath);
					var fileTask = model.File.CopyToAsync(stream);

					PhotoModel photo = new()
					{
						PhotoName = model.PhotoName,
						PhotoCode = imageCode,
						PhotoDescription = model.PhotoDescription,
					};

					//Adds a 'photo' to the database (actually just a reference to the url)
					//await pRepository.AddPhoto(photo);
					await fileTask;
				}
			}

			return RedirectToAction("Index", "Photos");
		}

		[HttpGet]
		public IActionResult AddGroup()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AddGroup(GroupUploadVM model)
		{
            if (model.Files != null)
            {
				PhotoGroupModel group = new()
				{
					GroupName = model.GroupName,
					GroupDescription = model.GroupDescription
				};

				for (int i = 0; i < model.Files.Count; i++)
				{
					IFormFile file = model.Files[i];
					string imageCode = model.GroupName + i.ToString() + '_' + file.Length.GetHashCode().ToString()
						+ file.Headers.GetHashCode().ToString() + file.Name.GetHashCode().ToString() + Path.GetExtension(file.FileName);

					if (file.Length > 0)
					{
                        //Confirms that the directory exists and creates it if not
                        if (!Directory.Exists("./wwwroot/photos"))
                            Directory.CreateDirectory("./wwwroot/photos");

                        //Creates a file path to the 'photos' folder and append the target file name
                        var filePath = Path.Combine("./wwwroot/photos/", imageCode);

                        //Creates the target file and copies the data to it
                        using var stream = System.IO.File.Create(filePath);
                        var fileTask = file.CopyToAsync(stream);

						PhotoModel photo = new()
						{
							PhotoName = group.GroupName + i.ToString(),
							PhotoCode = imageCode,
							PhotoDescription = group.GroupDescription,
							Group = group
                        };

						group.Photos.Add(photo);

                        //Adds a 'photo' to the database (actually just a reference to the url)
                        //await pRepository.AddPhoto(photo);
                        await fileTask;
                    }
				}
				//Adds the group of photos to the database
                //await pRepository.AddGroup(group);
            }

            return RedirectToAction("Index", "Photos");
		}



		//These are currently non-functional for a number of reasons

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
