using GrassHopper.Models;

namespace GrassHopper.Data.Repositories
{
    public interface IPortfolioRepository
    {
        public Task<Portfolio> GetPortfolio(int id);
        public Task<List<PortfolioVM>> GetAllPortfolios();
        public Task<List<PortfolioVM>> GetHiddenPortfolios();
        public Task<List<Portfolio>> GetAllPortfoliosAdmin();
        public Task<List<PortfolioVM>> GetPortfoliosByTag(string tag);
        public Task<int> AddPortfolio(Portfolio portfolio);
        public Task<int> UpdatePortfolio(Portfolio portfolio);
        public Task<int> HidePortfolio(int id);
        public Task<int> RestorePortfolio(int id);
        public Task<int> DeletePortfolio(int id);
        public Task<int> AddPortfolioTag(int id, string tag);
        public Task<int> RemovePortfolioTag(int id, string tag);
    }
}
