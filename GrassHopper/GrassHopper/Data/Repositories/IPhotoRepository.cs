using GrassHopper.Models;

namespace GrassHopper.Data.Repositories
{
    public interface IPhotoRepository
    {
        public Task<Photo> GetPhoto(int id);
        public Task<PhotoGroupModel> GetPhotoGroup(int id);
        public List<Photo> GetAllPhotos();
        public Task<int> AddPhoto(Photo photo);
        public Task<int> UpdatePhoto(Photo photo);
        public Task<int> AddGroup(PhotoGroupModel group);
        public Task<int> UpdateGroup(PhotoGroupModel group);
        public Task<int> DeletePhoto(int id);
    }
}
