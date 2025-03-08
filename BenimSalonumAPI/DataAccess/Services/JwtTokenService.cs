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
using System.Security.Cryptography;

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

        public async Task<string> GenerateToken(int userId)
        {
            var dbUser = await _context.Kullanicilar
                .Where(u => u.Id == userId)
                .Select(u => new { u.KullaniciAdi, u.Gorevi })
                .FirstOrDefaultAsync();

            if (dbUser == null)
            {
                throw new InvalidOperationException("Kullanıcı veritabanında bulunamadı!");
            }

            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
        new Claim(ClaimTypes.Name, dbUser.KullaniciAdi),
        new Claim(ClaimTypes.Role, dbUser.Gorevi)
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiresAt = DateTime.UtcNow.AddHours(2);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
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
                Role = dbUser.Gorevi // ✅ Password alanı kaldırıldı!
            };

            await _context.UserJwtTokens.AddAsync(userToken);
            await _context.SaveChangesAsync();

            return tokenString;
        }






        // **🔹 2️⃣ TOKEN DOĞRULAMA METODU**
        public async Task<bool> ValidateToken(string token)
        {
            var existingToken = await _context.UserJwtTokens.FirstOrDefaultAsync(t => t.Token == token);

            if (existingToken == null)
            {
                Console.WriteLine("❌ Token veritabanında bulunamadı.");
                return false;
            }

            if (existingToken.Expiration < DateTime.UtcNow)
            {
                Console.WriteLine("❌ Token süresi dolmuş!");
                _context.UserJwtTokens.Remove(existingToken);
                await _context.SaveChangesAsync();
                return false;
            }

            return true;
        }
    }
}
