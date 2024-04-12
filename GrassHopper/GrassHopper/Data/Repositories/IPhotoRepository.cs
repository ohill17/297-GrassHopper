using GrassHopper.Models;

namespace GrassHopper.Data.Repositories
{
    public interface IPhotoRepository
    {
        public Task<Photo> GetPhoto(int id);
        public Task<PhotoGroup> GetPhotoGroup(int id);
        public Task<List<Photo>> GetAllPhotos();
        public Task<List<PhotoGroup>> GetAllGroups();
        public Task<int> AddPhoto(Photo photo);
        public Task<int> UpdatePhoto(Photo photo);
        public Task<int> AddGroup(PhotoGroup group);
        public Task<int> UpdateGroup(PhotoGroup group);
        public Task<int> DeletePhoto(int id);
    }
}
