using GrassHopper.Models;

namespace GrassHopper.Data.Repositories
{
    public interface IAppSettingsRepository
    {
        public Task<AppSettings> GetAppSettings(int id);

        public Task<AppSettings> GetAppSettingsByFacebookAppId(string appId);
        public Task<List<AppSettings>> GetAllAppSettings();
        public Task<int> AddAppSettings(AppSettings appSettings);
        public Task<int> UpdateAppSettings(AppSettings appSettings);
        public Task<int> DeleteAppSettings(int id);
    }
}
