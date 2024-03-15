using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GH.Data;
using GH.Models;

namespace GH.Data.Repositories
{
    public interface IPhotosRepository
    {
        public List<PhotoModel> GetPhotos();
        public Task<PhotoModel> GetPhotosByIdAsync(int id);
        public Task<int> StorePhotosAsync(PhotoModel photo);
        public int DeletePhotos(int id);
    }

    public class PhotosRepository : IPhotosRepository
    {
        private readonly AppDbContext context;

        public PhotosRepository(AppDbContext c)
        {
            context = c;
        }

        public async Task<PhotoModel> GetPhotosByIdAsync(int id)
        {
            var photo = await context.Photos.FindAsync(id);
            return photo;
        }

        public List<PhotoModel> GetPhotos()
        {
            return context.Photos.ToList();
        }

        public async Task<int> StorePhotosAsync(PhotoModel model)
        {
            await context.AddAsync(model);
            return await context.SaveChangesAsync();
        }

        public int DeletePhotos(int photoId)
        {
            PhotoModel photo = context.Photos.SingleOrDefault(p => p.PhotoId == photoId);
            context.Photos.Remove(photo);
            return context.SaveChanges();
        }
    }
}
