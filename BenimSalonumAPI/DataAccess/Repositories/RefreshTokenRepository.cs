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

        // 📌 **Refresh Token'ı Veritabanına Kaydet**
        public async Task SaveRefreshToken(RefreshToken token)
        {
            await _context.RefreshTokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        // 📌 **Belirtilen Token'ı Getir**
        public async Task<RefreshToken> GetRefreshToken(string token)
        {
            return await _context.RefreshTokens
                .AsNoTracking() // ✅ Performans artırmak için
                .FirstOrDefaultAsync(x => x.Token == token);
        }

        // 📌 **Kullanıcının Tüm Refresh Token'larını Geçersiz Yap**
        public async Task RevokeUserRefreshTokens(string userId)
        {
            var userTokens = await _context.RefreshTokens
                .Where(t => t.UserId == userId && !t.IsRevoked)
                .ToListAsync();

            if (!userTokens.Any()) return; // ✅ Kullanıcının aktif tokenı yoksa çık

            userTokens.ForEach(token => token.IsRevoked = true); // ✅ Tüm tokenları iptal et

            await _context.SaveChangesAsync();
            Console.WriteLine($"✅ Kullanıcının ({userId}) tüm refresh tokenları iptal edildi.");
        }

        // 📌 **Kullanıcının Tüm Access Token'larını Geçersiz Yap**
        public async Task RevokeUserAccessTokens(int userId)
        {
            var userTokens = await _context.UserJwtTokens
                .Where(t => t.UserId == userId)
                .ToListAsync();

            if (!userTokens.Any()) return; // ✅ Kullanıcının Access Token'ı yoksa çık

            _context.UserJwtTokens.RemoveRange(userTokens);
            await _context.SaveChangesAsync();
            Console.WriteLine($"✅ Kullanıcının ({userId}) tüm access tokenları silindi.");
        }
        public async Task RevokeDuplicateDeviceTokens(string userId, string deviceName, string platform, string userAgent)
        {
            var existingTokens = await _context.RefreshTokens
                .Where(x => x.UserId == userId && !x.IsRevoked
                    && x.DeviceName == deviceName
                    && x.Platform == platform
                    && x.UserAgent == userAgent)
                .ToListAsync();

            if (existingTokens.Any())
            {
                foreach (var token in existingTokens)
                    token.IsRevoked = true;

                await _context.SaveChangesAsync();
                Console.WriteLine($"🔄 Aynı cihazdan gelen eski refresh tokenlar iptal edildi.");
            }
        }

    }
}
