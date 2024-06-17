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
            var theToken = await dbContext.Tokens.FindAsync(id);
            if (theToken != null) {
                dbContext.Tokens.Remove(theToken);
                return dbContext.SaveChanges();
            } else
            {
                throw new NullReferenceException();
            }
            
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
            Token oldToken = await dbContext.Tokens.FindAsync(token.TokenID);
            if (oldToken != null)
            {
                oldToken.TokenString = token.TokenString;
                oldToken.TokenLength = token.TokenLength;
                oldToken.TokenType = token.TokenType;
                oldToken.CreationTime = token.CreationTime;
            }
            dbContext.Tokens.Update(oldToken);
            return dbContext.SaveChanges();
        }
    }
}
