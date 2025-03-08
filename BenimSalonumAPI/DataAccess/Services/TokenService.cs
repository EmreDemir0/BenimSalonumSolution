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
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Secret"]);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),  // 📌 Access Token süresi (15 dakika)
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // 📌 **Refresh Token Üret**
        public RefreshToken GenerateRefreshToken(string userId)
        {
            using var rng = new RNGCryptoServiceProvider();
            var randomBytes = new byte[64];
            rng.GetBytes(randomBytes);
            return new RefreshToken
            {
                UserId = userId,
                Token = Convert.ToBase64String(randomBytes),
                Expires = DateTime.UtcNow.AddDays(7),  // 📌 Refresh Token süresi (7 gün)
                IsRevoked = false
            };
        }
    }
}
