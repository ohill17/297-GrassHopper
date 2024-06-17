using GrassHopper.Models;
using Microsoft.EntityFrameworkCore;

namespace GrassHopper.Data.Repositories
{
    public class AppSettingsRepository : IAppSettingsRepository
    {
        private AppDbContext dbContext;

        public AppSettingsRepository(AppDbContext dbCntxt)
        {
            dbContext = dbCntxt;
        }
        public async Task<int> AddAppSettings(AppSettings appSettings)
        {
            await dbContext.AppSettings.AddAsync(appSettings);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAppSettings(int id)
        {
            AppSettings appSettings = await GetAppSettings(id);
            dbContext.AppSettings.Remove(appSettings);
            return dbContext.SaveChanges();
        }

        public async Task<List<AppSettings>> GetAllAppSettings()
        {
            return await dbContext.AppSettings.ToListAsync();   
        }

        public async Task<AppSettings> GetAppSettings(int id)
        {
            return await dbContext.AppSettings.FindAsync(id);
        }

        public async Task<AppSettings> GetAppSettingsByFacebookAppId(string appId)
        {
            var ASList = dbContext.AppSettings
                .Where(a => a.FacebookAppId == appId)
                .ToList();
            if (ASList.Count > 0)
            {
                return ASList[0];
            } else
            {
                AppSettings newAS = new();
                newAS.FacebookAppId = "Not Found";
                return newAS;
            }

        }


        public Task<int> UpdateAppSettings(AppSettings appSettings)
        {
            throw new NotImplementedException();
        }
    }
}
