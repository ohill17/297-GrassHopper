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

		public async Task<IActionResult> Index()
		{
			var photos = await pRepository.GetAllUngrouped();
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
				string imageCode = MakeFileName(model.PhotoName, model.File);

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

					Photo photo = new()
					{
						PhotoName = model.PhotoName,
						PhotoCode = imageCode,
						PhotoDescription = model.PhotoDescription,
					};

					//Adds a 'photo' to the database (actually just a reference to the url)
					await pRepository.AddPhoto(photo);
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
				PhotoGroup group = new()
				{
					GroupName = model.GroupName,
					GroupDescription = model.GroupDescription
				};

				for (int i = 0; i < model.Files.Count; i++)
				{
					IFormFile file = model.Files[i];
					string imageCode = MakeFileName(model.GroupName, file);

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

						Photo photo = new()
						{
							PhotoName = group.GroupName + i.ToString(),
							PhotoCode = imageCode,
							PhotoDescription = group.GroupDescription,
							Group = group
                        };

						group.Photos.Add(photo);

                        await fileTask;
                    }
				}
				//Adds the group of photos to the database
                await pRepository.AddGroup(group);
            }

            return RedirectToAction("Groups", "Photos");
		}

		private string MakeFileName(string prefix, IFormFile file)
		{
			return prefix + '_' + file.Length.GetHashCode().ToString() + file.Name.GetHashCode().ToString()
				+ DateTime.Now.GetHashCode().ToString() + Path.GetExtension(file.FileName); ;
		}

		public async Task<IActionResult> Groups()
		{
			var groups = await pRepository.GetAllGroups();
			return View(groups);
		}

		[HttpGet]
		public async Task<IActionResult> DeletePhoto(int photoId)
		{
			var photo = await pRepository.GetPhoto(photoId);
			return View(photo);
		}

		public async Task<IActionResult> HidePhoto(int photoId)
		{
			await pRepository.HidePhoto(photoId);
			return RedirectToAction("Index", "Photos");
		}

		[HttpPost]
		public async Task<IActionResult> DeletePhoto(Photo photo)
		{
			await pRepository.DeletePhoto(photo.PhotoId);
			return RedirectToAction("Index", "Photos");
		}

		[HttpGet]
		public async Task<IActionResult> DeleteGroup(int groupId)
		{
			var group = await pRepository.GetPhotoGroup(groupId);
			return View(group);
		}

		public async Task<IActionResult> HideGroup(int groupId)
		{
			await pRepository.HideGroup(groupId);
			return RedirectToAction("Groups", "Photos");
		}

		[HttpPost]
		public async Task<IActionResult> DeleteGroup(PhotoGroup group)
		{
			await pRepository.DeleteGroup(group.GroupId);
			return RedirectToAction("Groups", "Photos");
		}

		[HttpGet]
		public async Task<IActionResult> HiddenPhotos()
		{
			var photos = await pRepository.GetHiddenPhotos();
			return View(photos);
		}

		public async Task<IActionResult> RestorePhoto(int photoId)
		{
			await pRepository.RestorePhoto(photoId);
			return RedirectToAction("Index", "Photos");
		}

		[HttpGet]
		public async Task<IActionResult> HiddenGroups()
		{
			var groups = await pRepository.GetHiddenGroups();
			return View(groups);
		}

		public async Task<IActionResult> RestoreGroup(int groupId)
		{
			await pRepository.RestoreGroup(groupId);
			return RedirectToAction("Groups", "Photos");
		}
    }
}
