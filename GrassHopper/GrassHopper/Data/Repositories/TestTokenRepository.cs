using GrassHopper.Models;

namespace GrassHopper.Data.Repositories
{
    public class TestTokenRepository : ITokenRepository
    {

        private List<Token> tokens = new();
        public async Task<int> AddToken(Token token)
        {
            token.TokenID = tokens.Count;
            tokens.Add(token);
            return tokens.IndexOf(token);
        }

        public async Task<int> DeleteToken(int id)
        {
            tokens.RemoveAt(id);
            return tokens.Count;
        }

        public async Task<List<Token>> GetAllTokens()
        {
            return tokens;
        }

        public async Task<Token> GetToken(int id)
        {
            return tokens.Where(t => t.TokenID == id).FirstOrDefault();
        }

        public Task<int> UpdateToken(Token token)
        {
            throw new NotImplementedException();
        }
    }
}
