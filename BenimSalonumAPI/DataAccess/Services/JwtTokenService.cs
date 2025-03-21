using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BenimSalonum.Entities.Tables;
using BenimSalonumAPI.DataAccess.Context;

namespace BenimSalonumAPI.DataAccess.Services
{
    public class JwtTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly BenimSalonumContext _context;

        public JwtTokenService(IConfiguration configuration, BenimSalonumContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // ✅ **Access Token Oluşturma Metodu**
        public async Task<string> GenerateToken(int userId)
        {
            var dbUser = await _context.Kullanicilar
                .Where(u => u.Id == userId)
                .Select(u => new { u.KullaniciAdi, u.Gorevi, u.Id })
                .FirstOrDefaultAsync();

            if (dbUser == null)
            {
                throw new InvalidOperationException("❌ Kullanıcı veritabanında bulunamadı!");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()),
                new Claim(ClaimTypes.Name, dbUser.KullaniciAdi),
                new Claim(ClaimTypes.Role, dbUser.Gorevi)
            };

            var secretKey = _configuration["JwtSettings:Secret"]; // ✅ Secret Key Güncellendi!
            if (string.IsNullOrWhiteSpace(secretKey))
            {
                throw new InvalidOperationException("❌ JWT Secret key ayarlanmamış! Lütfen appsettings.json içine ekleyin.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // ✅ **Token süresini UTC olarak ayarla**
            var expiresAt = DateTime.UtcNow.AddHours(2);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],   // ✅ Doğru yapılandırma
                audience: _configuration["JwtSettings:Audience"], // ✅ Doğru yapılandırma
                claims: claims,
                expires: expiresAt,
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            // ✅ **Access Token'ı DB'ye kaydet**
            var userToken = new UserJwtToken
            {
                UserId = userId,
                Token = tokenString,
                Expiration = expiresAt,
                Username = dbUser.KullaniciAdi,
                Role = dbUser.Gorevi
            };

            await _context.UserJwtTokens.AddAsync(userToken);
            await _context.SaveChangesAsync();

            Console.WriteLine($"✅ Yeni Access Token oluşturuldu: {tokenString}");
            return tokenString;
        }

        // ✅ **Refresh & Access Token Doğrulama Metodu**
        public async Task<bool> ValidateToken(string token)
        {
            var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);
            var accessToken = await _context.UserJwtTokens.FirstOrDefaultAsync(t => t.Token == token);

            if (accessToken == null && refreshToken == null)
            {
                Console.WriteLine("❌ Token veritabanında bulunamadı.");
                return false;
            }

            if (refreshToken != null)
            {
                if (refreshToken.IsRevoked)
                {
                    Console.WriteLine("❌ Refresh Token iptal edilmiş!");
                    return false;
                }

                if (refreshToken.Expires < DateTime.UtcNow)
                {
                    Console.WriteLine("❌ Refresh Token süresi dolmuş! Veritabanından siliniyor.");
                    _context.RefreshTokens.Remove(refreshToken);
                    await _context.SaveChangesAsync();
                    return false;
                }
            }

            if (accessToken != null)
            {
                if (accessToken.Expiration < DateTime.UtcNow)
                {
                    Console.WriteLine("❌ Access Token süresi dolmuş! Veritabanından siliniyor.");
                    _context.UserJwtTokens.Remove(accessToken);
                    await _context.SaveChangesAsync();
                    return false;
                }
            }

            Console.WriteLine($"✅ Token geçerli: {token}");
            return true;
        }
    }
}
