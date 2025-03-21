using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using BenimSalonum.Entities.Tables;
using Microsoft.Extensions.Configuration;

namespace BenimSalonumAPI.DataAccess.Services
{
    public class TokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        // 📌 **Access Token Üret**
        public string GenerateAccessToken(string userId)
        {
            var secretKey = _config["JwtSettings:Secret"]; // ✅ Doğru Secret Key alınıyor
            if (string.IsNullOrWhiteSpace(secretKey))
            {
                throw new InvalidOperationException("❌ JWT Secret key ayarlanmamış! Lütfen appsettings.json içine ekleyin.");
            }

            var key = Encoding.UTF8.GetBytes(secretKey);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2), // ✅ UTC kullanıyoruz
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                Issuer = _config["JwtSettings:Issuer"],   // ✅ Issuer ekledik
                Audience = _config["JwtSettings:Audience"], // ✅ Audience ekledik

            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // 📌 **Refresh Token Üret**
        public RefreshToken GenerateRefreshToken(string userId, string ipAddress, string userAgent, string deviceName, string platform)
        {
            var randomBytes = new byte[64];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return new RefreshToken
            {
                UserId = userId,
                Token = Convert.ToBase64String(randomBytes),
                Expires = DateTime.UtcNow.AddDays(7),
                IsRevoked = false,
                CreatedAt = DateTime.UtcNow,
                IpAddress = ipAddress,
                UserAgent = userAgent,
                DeviceName = deviceName,
                Platform = platform
            };
        }


    }
}
