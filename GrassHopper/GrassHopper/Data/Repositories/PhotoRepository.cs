using GrassHopper.Models;
using Microsoft.EntityFrameworkCore;

namespace GrassHopper.Data.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private AppDbContext dbContext;
        
        public PhotoRepository(AppDbContext dbCntxt)
        {
            dbContext = dbCntxt;
        }
        public async Task<int> AddPhoto(PhotoModel photo)
        {
            await dbContext.Photos.AddAsync(photo);
            return dbContext.SaveChanges();
        }

        public async Task<int> DeletePhoto(int id)
        {
            throw new NotImplementedException();
        }

        public List<PhotoModel> GetAllPhotos()
        {
            return new();//dbContext.Photos.ToList(); //Database currently not functional
        }

        public async Task<PhotoModel> GetPhoto(int id)
        {
            return new();//dbContext.Photos.Find(id);//Database currently not functional
        }

        public async Task<int> UpdatePhoto(PhotoModel photo)
        {
            throw new NotImplementedException();
        }
    }
}
