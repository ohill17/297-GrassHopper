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
			throw new NotImplementedException();
		}

		public async Task<int> AddGroup(PhotoGroup group)
		{
			await dbContext.PhotoGroups.AddAsync(group);
			return dbContext.SaveChanges();
		}

		public async Task<int> UpdateGroup(PhotoGroup group)
		{
			throw new NotImplementedException();
		}

		public async Task<List<Photo>> GetAllPhotos()
		{
			return await dbContext.Photos
				.Where(p => p.IsHidden == false
					&& (p.Group == null || p.Group.IsHidden == false))
				.Include(p => p.Group)
				.ToListAsync();
		}

		public async Task<List<Photo>> GetHiddenPhotos()
		{
			return await dbContext.Photos
				.Where(p => p.IsHidden)
				.Include(p => p.Group)
				.ToListAsync();
		}

		public async Task<Photo> GetPhoto(int id)
		{
			return await dbContext.Photos.FindAsync(id);
		}

		public async Task<PhotoGroup> GetPhotoGroup(int id)
		{
			return await dbContext.PhotoGroups.Where(g => g.GroupId == id)
				.Include(g => g.Photos)
				.FirstAsync();
		}

		public async Task<List<PhotoGroup>> GetAllGroups()
		{
			return await dbContext.PhotoGroups
				.Where(g => g.IsHidden == false)
				.Include(g => g.Photos)
				.ToListAsync();
		}

		public async Task<List<PhotoGroup>> GetHiddenGroups()
		{
			return await dbContext.PhotoGroups
				.Where(g => g.IsHidden)
				.Include(g => g.Photos)
				.ToListAsync();
		}

		public async Task<List<Photo>> GetAllUngrouped()
		{
			return await dbContext.Photos
				.Where(p => p.Group == null)
				.ToListAsync();
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
	}
}
