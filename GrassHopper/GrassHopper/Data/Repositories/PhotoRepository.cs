using GrassHopper.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace GrassHopper.Data.Repositories
{
	public class PhotoRepository : IPhotoRepository
	{
		private AppDbContext dbContext;

		public PhotoRepository(AppDbContext dbCntxt)
		{
			dbContext = dbCntxt;
		}
		public async Task<int> AddPhoto(Photo photo)
		{
			await dbContext.Photos.AddAsync(photo);
			return dbContext.SaveChanges();
		}

		public async Task<int> UpdatePhoto(Photo photo)
		{
			Photo oldPhoto = await GetPhoto(photo.PhotoId);
			oldPhoto.PhotoName = photo.PhotoName;
			oldPhoto.PhotoDescription = photo.PhotoDescription;
			dbContext.Photos.Update(oldPhoto);
			return dbContext.SaveChanges();
		}

		public async Task<int> AddGroup(PhotoGroup group)
		{
			await dbContext.PhotoGroups.AddAsync(group);
			return dbContext.SaveChanges();
		}

		public async Task<int> UpdateGroup(PhotoGroup group)
		{
			PhotoGroup oldGroup = await GetPhotoGroup(group.GroupId);
			oldGroup.GroupName = group.GroupName;
			oldGroup.GroupDescription = group.GroupDescription;
			dbContext.PhotoGroups.Update(oldGroup);
			return dbContext.SaveChanges();
		}

		public async Task<List<PhotoVM>> GetAllPhotos(PhotoSize size)
		{
			var photos = await dbContext.Photos
				.Where(p => p.IsHidden == false
					&& (p.Group == null || p.Group.IsHidden == false))
				.Include(p => p.Group)
				.ToListAsync();
			List<PhotoVM> photoVMs = new List<PhotoVM>();
			foreach(Photo p in photos)
			{
				photoVMs.Add(new PhotoVM(p, size));
			}
			return photoVMs;
		}

		public async Task<List<PhotoVM>> GetHiddenPhotos(PhotoSize size)
		{
			var photos = await dbContext.Photos
				.Where(p => p.IsHidden)
				.Include(p => p.Group)
				.ToListAsync();
            List<PhotoVM> photoVMs = new List<PhotoVM>();
            foreach (Photo p in photos)
            {
                photoVMs.Add(new PhotoVM(p, size));
            }
            return photoVMs;
        }

		public async Task<Photo> GetPhoto(int id)
		{
			return await dbContext.Photos.Where(p => p.PhotoId == id).Include(p => p.Group).FirstAsync();
		}

		public async Task<PhotoGroup> GetPhotoGroup(int id)
		{
			return await dbContext.PhotoGroups.Where(g => g.GroupId == id)
				.Include(g => g.Photos)
				.FirstAsync();
		}

		public async Task<List<GroupVM>> GetAllGroups(PhotoSize size)
		{
			var groups = await dbContext.PhotoGroups
				.Where(g => g.IsHidden == false)
				.Include(g => g.Photos)
				.ToListAsync();
			return GVMMaker.MakeGroupVM(groups, size);
		}

		public async Task<List<GroupVM>> GetHiddenGroups(PhotoSize size)
		{
			var groups = await dbContext.PhotoGroups
				.Where(g => g.IsHidden)
				.Include(g => g.Photos)
				.ToListAsync();
			return GVMMaker.MakeGroupVM(groups, size);
		}

		public async Task<List<PhotoVM>> GetAllUngrouped(PhotoSize size)
		{
			var photos = await dbContext.Photos
				.Where(p => p.Group == null
    					&& p.IsHidden == false)
				.ToListAsync();
            List<PhotoVM> photoVMs = new List<PhotoVM>();
            foreach (Photo p in photos)
            {
                photoVMs.Add(new PhotoVM(p, size));
            }
            return photoVMs;
        }

		public async Task<int> HidePhoto(int id)
		{
			Photo photo = await GetPhoto(id);
			photo.IsHidden = true;
			dbContext.Photos.Update(photo);
			return dbContext.SaveChanges();
		}

		public async Task<int> RestorePhoto(int id)
		{
			Photo photo = await GetPhoto(id);
			photo.IsHidden = false;
			dbContext.Photos.Update(photo);
			return dbContext.SaveChanges();
		}

		public async Task<int> DeletePhoto(int id)
		{
			Photo photo = await GetPhoto(id);
			//Tries to remove the file found in the database
			string filePath = "./wwwroot/photos/" + photo.PhotoCode;
			try
			{
				File.Delete(filePath);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
			dbContext.Photos.Remove(photo);
			return dbContext.SaveChanges();
		}

		public async Task<int> HideGroup(int id)
		{
			PhotoGroup group = await GetPhotoGroup(id);
			group.IsHidden = true;
			dbContext.PhotoGroups.Update(group);
			return dbContext.SaveChanges();
		}

		public async Task<int> RestoreGroup(int id)
		{

			PhotoGroup group = await GetPhotoGroup(id);
			group.IsHidden = false;
			dbContext.PhotoGroups.Update(group);
			return dbContext.SaveChanges();
		}

		public async Task<int> DeleteGroup(int id)
		{
			PhotoGroup group = await GetPhotoGroup(id);
			foreach (Photo photo in group.Photos)
			{
				//Tries to remove every file found in the database
				string filePath = "./wwwroot/photos/" + photo.PhotoCode;
				try
				{
					File.Delete(filePath);
				}
				catch { }
				dbContext.Photos.Remove(photo);
			}
			dbContext.PhotoGroups.Remove(group);
			return dbContext.SaveChanges();
		}

		public async Task<int> RemoveFromGroup(int photoId)
		{
			Photo photo = await GetPhoto(photoId);
			PhotoGroup group;
			try
			{
				group = await GetPhotoGroup(photo.Group.GroupId);
			}
			catch { return 100; } //Could not find group, or photo was not in a group
			group.Photos.Remove(photo);
			photo.Group = null;
			dbContext.Photos.Update(photo);
			dbContext.PhotoGroups.Update(group);
			return dbContext.SaveChanges();
		}

		public async Task<int> AddToGroup(int photoId, int groupId)
		{
			Photo photo = await GetPhoto(photoId);
			PhotoGroup group = await GetPhotoGroup(groupId);
			photo.Group = group;
			group.Photos.Add(photo);
			dbContext.Photos.Update(photo);
			dbContext.PhotoGroups.Update(group);
			return dbContext.SaveChanges();
		}

		public async Task<int> BreakGroup(int groupId)
		{
			PhotoGroup group = await GetPhotoGroup(groupId);
			foreach(Photo p in group.Photos)
			{
				Photo photo = await GetPhoto(p.PhotoId);
				photo.Group = null;
				dbContext.Photos.Update(photo);
			}
			group.Photos.Clear();
			dbContext.PhotoGroups.Update(group);
			return dbContext.SaveChanges();
		}
	}
}
