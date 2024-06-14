using GrassHopper.Models;
using GrassHopper.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace GrassHopper.Data.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private AppDbContext dbContext;
        
        public TokenRepository(AppDbContext dbCntxt)
        {
            dbContext = dbCntxt;
        }
        public async Task<int> AddToken(Token token)
        {
            await dbContext.Tokens.AddAsync(token);
            return dbContext.SaveChanges();
        }

        public async Task<int> DeleteToken(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Token>> GetAllTokens()
        {
            return await dbContext.Tokens.ToListAsync();
        }

        public async Task<Token> GetToken(int id)
        {
            return await dbContext.Tokens.FindAsync(id);
        }

        public async Task<int> UpdateToken(Token token)
        {
            throw new NotImplementedException();
        }
    }
}
