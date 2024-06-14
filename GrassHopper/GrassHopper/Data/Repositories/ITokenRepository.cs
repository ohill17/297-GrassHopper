using GrassHopper.Models;

namespace GrassHopper.Data.Repositories
{
    public interface ITokenRepository
    {
        public Task<Token> GetToken(int id);
        public Task<List<Token>> GetAllTokens();
        public Task<int> AddToken(Token token);
        public Task<int> UpdateToken(Token token);
        public Task<int> DeleteToken(int id);
    }
}
