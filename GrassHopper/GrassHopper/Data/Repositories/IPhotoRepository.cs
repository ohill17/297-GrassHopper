using GrassHopper.Models;
using System.ComponentModel;

namespace GrassHopper.Data.Repositories
{
    public interface IPhotoRepository
    {
        public Task<Photo> GetPhoto(int id);
        public Task<PhotoGroup> GetPhotoGroup(int id);
        public Task<List<Photo>> GetAllPhotos();
        public Task<List<Photo>> GetHiddenPhotos();
        public Task<List<PhotoGroup>> GetAllGroups();
        public Task<List<PhotoGroup>> GetHiddenGroups();
        public Task<List<Photo>> GetAllUngrouped();
        public Task<int> AddPhoto(Photo photo);
        public Task<int> UpdatePhoto(Photo photo);
        public Task<int> AddGroup(PhotoGroup group);
        public Task<int> UpdateGroup(PhotoGroup group);
        public Task<int> HidePhoto(int id); //Sets a 'hidden' flag, but keeps the photo and DB item
        public Task<int> RestorePhoto(int id); //Removes 'hidden' flag
        public Task<int> DeletePhoto(int id); //Fully removes photo from storage and DB
        public Task<int> HideGroup(int id);
        public Task<int> RestoreGroup(int id);
        public Task<int> DeleteGroup(int id);
        public Task<int> RemoveFromGroup(int photoId);
        public Task<int> AddToGroup(int photoId, int groupId);
        public Task<int> BreakGroup(int groupId); //Dissolves a group, leaving behind all photos that were part of it
    }
}
