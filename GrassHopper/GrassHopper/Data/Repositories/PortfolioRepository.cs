using GrassHopper.Models;
using Microsoft.EntityFrameworkCore;

namespace GrassHopper.Data.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private AppDbContext dbContext;

        public PortfolioRepository(AppDbContext dbCntxt)
        {
            dbContext = dbCntxt;
        }

        public async Task<int> AddPortfolio(Portfolio portfolio)
        {
            await dbContext.Portfolios.AddAsync(portfolio);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> DeletePortfolio(int id)
        {
            Portfolio oldPortfolio = await GetPortfolio(id);
            dbContext.Portfolios.Remove(oldPortfolio);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<List<PortfolioVM>> GetAllPortfolios()
        {
            var portfolios = await dbContext.Portfolios
                .Include(p => p.PortfolioThumbnail)
                .Include(p => p.PortfolioPGroups)
                .ThenInclude(g => g.Photos)
                .Where(p => !p.IsHidden)
                .ToListAsync();
            return VMMaker.MakePortfolioVM(portfolios);
        }

        public async Task<List<PortfolioVM>> GetHiddenPortfolios()
        {
            var portfolios = await dbContext.Portfolios
                .Include(p => p.PortfolioThumbnail)
                .Include(p => p.PortfolioPGroups)
                .ThenInclude(g => g.Photos)
                .Where(p => p.IsHidden)
                .ToListAsync();
            return VMMaker.MakePortfolioVM(portfolios);
        }

        public async Task<List<Portfolio>> GetAllPortfoliosAdmin()
        {
            return await dbContext.Portfolios
                .Include(p => p.PortfolioThumbnail)
                .Include(p => p.PortfolioPGroups)
                .ThenInclude(g => g.Photos)
                .ToListAsync();
        }

        public async Task<List<PortfolioVM>> GetPortfoliosByTag(string tag)
        {
            var portfolios = await dbContext.Portfolios
                .Include(p => p.PortfolioThumbnail)
                .Include(p => p.PortfolioPGroups)
                .ThenInclude(g => g.Photos)
                .Where(p => !p.IsHidden && p.PortfolioTags.Any(t => t.TagText == tag))
                .ToListAsync();
            return VMMaker.MakePortfolioVM(portfolios);
        }

        public async Task<Portfolio> GetPortfolio(int id)
        {
            return await dbContext.Portfolios
                .Include(p => p.PortfolioThumbnail)
                .Include(p => p.PortfolioPGroups)
                .ThenInclude(g => g.Photos)
                .Include(p => p.PortfolioTags)
                .Where(p => p.PortfolioId == id)
                .FirstAsync();
        }

        public async Task<int> HidePortfolio(int id)
        {
            Portfolio portfolio = await GetPortfolio(id);
            portfolio.IsHidden = true;
            dbContext.Portfolios.Update(portfolio);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> RestorePortfolio(int id)
        {
            Portfolio portfolio = await GetPortfolio(id);
            portfolio.IsHidden = false;
            dbContext.Portfolios.Update(portfolio);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdatePortfolio(Portfolio portfolio)
        {
            Portfolio oldPortfolio = await GetPortfolio(portfolio.PortfolioId);
            oldPortfolio.PortfolioName = portfolio.PortfolioName;
            oldPortfolio.PortfolioSummary = portfolio.PortfolioSummary;
            oldPortfolio.PortfolioDate = portfolio.PortfolioDate;
            oldPortfolio.PortfolioThumbnail = portfolio.PortfolioThumbnail;
            oldPortfolio.PortfolioPGroups = portfolio.PortfolioPGroups;
            oldPortfolio.PortfolioFormat = portfolio.PortfolioFormat;
            dbContext.Portfolios.Update(oldPortfolio);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> AddPortfolioTag(int id, string tag)
        {
            Portfolio portfolio = await GetPortfolio(id);
            portfolio.PortfolioTags.Add(new() { TagText = tag });
            dbContext.Portfolios.Update(portfolio);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> RemovePortfolioTag(int id, string tag)
        {
            Portfolio portfolio = await GetPortfolio(id);
            portfolio.PortfolioTags.Remove(new() { TagText = tag });
            dbContext.Portfolios.Update(portfolio);
            return await dbContext.SaveChangesAsync();
        }
    }
}
