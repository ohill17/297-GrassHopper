using GrassHopper.Models;

namespace GrassHopper.Data.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        public async Task<int> AddPhoto(PhotoModel photo)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeletePhoto(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PhotoModel>> GetAllPhotos()
        {
            throw new NotImplementedException();
        }

        public async Task<PhotoModel> GetPhoto(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdatePhoto(PhotoModel photo)
        {
            throw new NotImplementedException();
        }
    }
}
