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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

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
            var photos = await pRepository.GetAllUngrouped(PhotoSize.Medium);
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
                string extension = Path.GetExtension(model.File.FileName);

                if (model.File.Length > 0)
                {
                    //Confirms that the directory exists and creates it if not
                    if (!Directory.Exists("./wwwroot/photos"))
                        Directory.CreateDirectory("./wwwroot/photos");

                    //Creates a file path to the 'photos' folder and append the target file name
                    var filePathSM = Path.Combine("./wwwroot/photos/", imageCode + "SM" + extension);
                    var filePathMD = Path.Combine("./wwwroot/photos/", imageCode + "MD" + extension);
                    var filePathLG = Path.Combine("./wwwroot/photos/", imageCode + "LG" + extension);

                    //Creates the target file and copies the data to it
                    if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                        throw new Exception();
                    using (var memStream = new MemoryStream())
                    {
                        await model.File.CopyToAsync(memStream);
                        using var img = Image.FromStream(memStream);
                        Bitmap bmpPhoto = new(img);
                        Image image = ScaleImage(bmpPhoto, 256);
                        image.Save(filePathSM);

                        image = ScaleImage(bmpPhoto, 512);
                        image.Save(filePathMD);

                        image = ScaleImage(bmpPhoto, 1024);
                        image.Save(filePathLG);
                    }

                    Photo photo = new()
                    {
                        PhotoName = model.PhotoName,
                        PhotoCode = imageCode,
                        Extension = extension,
                        PhotoDescription = model.PhotoDescription,
                        Group = null
                    };

                    //Adds a 'photo' to the database (actually just a reference to the url)
                    await pRepository.AddPhoto(photo);
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
                    string extension = Path.GetExtension(file.FileName);

                    if (file.Length > 0)
                    {
                        //Confirms that the directory exists and creates it if not
                        if (!Directory.Exists("./wwwroot/photos"))
                            Directory.CreateDirectory("./wwwroot/photos");

                        //Creates a file path to the 'photos' folder and append the target file name
                        var filePathSM = Path.Combine("./wwwroot/photos/", imageCode + "SM" + extension);
                        var filePathMD = Path.Combine("./wwwroot/photos/", imageCode + "MD" + extension);
                        var filePathLG = Path.Combine("./wwwroot/photos/", imageCode + "LG" + extension);

                        //Creates the target file and copies the data to it
                        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                            throw new Exception();
                        using (var memStream = new MemoryStream())
                        {
                            await file.CopyToAsync(memStream);
                            using var img = Image.FromStream(memStream);
                            Bitmap bmpPhoto = new(img);
                            Image image = ScaleImage(bmpPhoto, 256);
                            image.Save(filePathSM);

                            image = ScaleImage(bmpPhoto, 512);
                            image.Save(filePathMD);

                            image = ScaleImage(bmpPhoto, 1024);
                            image.Save(filePathLG);
                        }

                        Photo photo = new()
                        {
                            PhotoName = group.GroupName + i.ToString(),
                            PhotoCode = imageCode,
                            Extension = extension,
                            PhotoDescription = group.GroupDescription,
                            Group = group
                        };

                        group.Photos.Add(photo);
                    }
                }
                //Adds the group of photos to the database
                await pRepository.AddGroup(group);
            }

            return RedirectToAction("Groups", "Photos");
        }

        private static string MakeFileName(string prefix, IFormFile file)
        {
            return prefix + '_' + file.Length.GetHashCode().ToString() + Guid.NewGuid().ToString(); //Why was I not doing this before
        }

        //From https://learn.microsoft.com/en-us/answers/questions/760483/upload-image-with-resize
        public static Image ScaleImage(Image image, int maxWidth)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                throw new Exception();
            var ratio = (double)maxWidth / image.Width;
            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);
            var newImage = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

        public async Task<IActionResult> Groups()
        {
            var groups = await pRepository.GetAllGroups();
            return View(groups);
        }

        [HttpGet]
        public async Task<IActionResult> EditPhoto(int photoId)
        {
            Photo photo = await pRepository.GetPhoto(photoId);
            ViewBag.Groups = await pRepository.GetAllGroups();
            return View(photo);
        }

        [HttpPost]
        public async Task<IActionResult> EditPhoto(Photo photo)
        {
            int result = await pRepository.UpdatePhoto(photo); //Potential hook for later
            return RedirectToAction("EditPhoto", "Photos", new { photoId = photo.PhotoId });
        }

        public async Task<IActionResult> UnGroup(int photoId)
        {
            int result = await pRepository.RemoveFromGroup(photoId);
            return RedirectToAction("EditPhoto", "Photos", new { photoId });
        }

        public async Task<IActionResult> AddToGroup(int photoId, int groupId)
        {
            int result = await pRepository.AddToGroup(photoId, groupId);
            return RedirectToAction("EditPhoto", "Photos", new { photoId });
        }

        [HttpGet]
        public async Task<IActionResult> EditGroup(int groupId)
        {
            PhotoGroup group = await pRepository.GetPhotoGroup(groupId);
            return View(group);
        }

        [HttpPost]
        public async Task<IActionResult> EditGroup(PhotoGroup group)
        {
            int result = await pRepository.UpdateGroup(group);
            return RedirectToAction("EditGroup", "Photos", new { groupId = group.GroupId });
        }

        public async Task<IActionResult> BreakGroup(int groupId)
        {
            int result = await pRepository.BreakGroup(groupId);
            await pRepository.DeleteGroup(groupId);
            return RedirectToAction("Index", "Photos");
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
            var photos = await pRepository.GetHiddenPhotos(PhotoSize.Medium);
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
