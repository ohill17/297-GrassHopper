using GrassHopper.Models;

namespace GrassHopper.Data.Repositories
{
    public interface IPhotoRepository
    {
        public Task<PhotoModel> GetPhoto(int id);
        public Task<PhotoGroupModel> GetPhotoGroup(int id);
        public List<PhotoModel> GetAllPhotos();
        public Task<int> AddPhoto(PhotoModel photo);
        public Task<int> UpdatePhoto(PhotoModel photo);
        public Task<int> AddGroup(PhotoGroupModel group);
        public Task<int> UpdateGroup(PhotoGroupModel group);
        public Task<int> DeletePhoto(int id);
    }
}
