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
        public async Task<int> AddPhoto(Photo photo)
        {
            await dbContext.Photos.AddAsync(photo);
            return dbContext.SaveChanges();
        }

        public async Task<int> UpdatePhoto(Photo photo)
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddGroup(PhotoGroupModel group)
        {
            await dbContext.PhotoGroups.AddAsync(group);
            return dbContext.SaveChanges();
        }

        public async Task<int> UpdateGroup(PhotoGroupModel group)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeletePhoto(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Photo>> GetAllPhotos()
        {
            return await dbContext.Photos.ToListAsync(); //Database currently not functional
        }

        public async Task<Photo> GetPhoto(int id)
        {
            return await dbContext.Photos.FindAsync(id); //Database currently not functional
        }

        public async Task<PhotoGroupModel> GetPhotoGroup(int id)
        {
            return await dbContext.PhotoGroups.Where(g => g.GroupId == id)
                .Include(g => g.Photos)
                .FirstAsync();
        }
    }
}
