using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BenimSalonumAPI.DataAccess.Context;
using BenimSalonum.Entities.Tables;

namespace BenimSalonumAPI.DataAccess.Repositories
{
    public class RefreshTokenRepository
    {
        private readonly BenimSalonumContext _context;

        public RefreshTokenRepository(BenimSalonumContext context)
        {
            _context = context;
        }

        public async Task SaveRefreshToken(RefreshToken token)
        {
            _context.RefreshTokens.Add(token);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken> GetRefreshToken(string token)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == token);
        }

        public async Task RevokeRefreshToken(string token)
        {
            var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == token);
            if (refreshToken != null)
            {
                refreshToken.IsRevoked = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
