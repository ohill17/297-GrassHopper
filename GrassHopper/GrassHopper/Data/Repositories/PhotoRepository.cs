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

        public async Task<int> AddGroup(PhotoGroup group)
        {
            await dbContext.PhotoGroups.AddAsync(group);
            return dbContext.SaveChanges();
        }

        public async Task<int> UpdateGroup(PhotoGroup group)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeletePhoto(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Photo>> GetAllPhotos()
        {
            return await dbContext.Photos.Include(p => p.Group).ToListAsync(); //Database currently not functional
        }

        public async Task<Photo> GetPhoto(int id)
        {
            return await dbContext.Photos.FindAsync(id); //Database currently not functional
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
                .Include(g => g.Photos)
                .ToListAsync();
        }
    }
}
