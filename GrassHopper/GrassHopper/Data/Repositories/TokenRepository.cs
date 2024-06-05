using GrassHopper.Models;
using GrassHopper.Data.Repositories;
using Microsoft.EntityFrameworkCore;

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

        public List<Token> GetAllTokens()
        {
            return dbContext.Tokens.ToList();
        }

        public async Task<Token> GetToken(int id)
        {
            return dbContext.Tokens.Find(id);
        }

        public async Task<int> UpdateToken(Token token)
        {
            throw new NotImplementedException();
        }
    }
}
