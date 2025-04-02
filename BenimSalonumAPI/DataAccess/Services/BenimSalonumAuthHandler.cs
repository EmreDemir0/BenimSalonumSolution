using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Linq;

namespace BenimSalonumAPI.DataAccess.Services
{
    public class BenimSalonumAuthOptions : AuthenticationSchemeOptions
    {
        public string SecretKey { get; set; }
    }

    public class BenimSalonumAuthHandler : AuthenticationHandler<BenimSalonumAuthOptions>
    {
        private readonly ILogger<BenimSalonumAuthHandler> _logger;

        public BenimSalonumAuthHandler(
            IOptionsMonitor<BenimSalonumAuthOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder)
            : base(options, logger, encoder)
        {
            _logger = logger.CreateLogger<BenimSalonumAuthHandler>();
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // 1. Authorization header'ı kontrol et
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                _logger.LogWarning("Authorization header bulunamadı");
                return Task.FromResult(AuthenticateResult.Fail("Authorization header eksik"));
            }

            string authHeader = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                _logger.LogWarning("Bearer token bulunamadı");
                return Task.FromResult(AuthenticateResult.Fail("Bearer token eksik"));
            }

            // 2. Token'ı çıkar
            string token = authHeader.Substring(7); // "Bearer " prefix'ini kaldır
            _logger.LogInformation($"Token çıkarıldı: {token.Substring(0, Math.Min(20, token.Length))}...");

            try
            {
                // 3. Token'ı doğrula
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(Options.SecretKey);

                // Debug - Token içeriğini görelim
                if (tokenHandler.CanReadToken(token))
                {
                    var readToken = tokenHandler.ReadJwtToken(token);
                    foreach (var claim in readToken.Claims)
                    {
                        _logger.LogInformation($"Claim: {claim.Type} = {claim.Value}");
                    }
                    
                    // exp claim'ini özellikle kontrol edelim
                    var expClaim = readToken.Claims.FirstOrDefault(c => c.Type == "exp");
                    if (expClaim != null)
                    {
                        _logger.LogInformation($"Expiration Claim bulundu: {expClaim.Value}");
                    }
                    else
                    {
                        _logger.LogWarning("Token içinde 'exp' claim'i bulunamadı!");
                    }
                }

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // Süre kontrolünü kapatmayı deneyelim
                    RequireExpirationTime = false
                }, out SecurityToken validatedToken);

                // 4. Doğrulanmış token'dan claim'leri al
                var jwtToken = (JwtSecurityToken)validatedToken;
                var identity = new ClaimsIdentity(jwtToken.Claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                _logger.LogInformation("Token doğrulama başarılı");
                return Task.FromResult(AuthenticateResult.Success(ticket));
            }
            catch (SecurityTokenExpiredException ex)
            {
                _logger.LogWarning($"Token süresi dolmuş: {ex.Message}");
                return Task.FromResult(AuthenticateResult.Fail("Token süresi dolmuş"));
            }
            catch (SecurityTokenInvalidSignatureException ex)
            {
                _logger.LogWarning($"Geçersiz token imzası: {ex.Message}");
                return Task.FromResult(AuthenticateResult.Fail("Geçersiz token imzası"));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Token doğrulama hatası: {ex.GetType().Name} - {ex.Message}");
                return Task.FromResult(AuthenticateResult.Fail("Token doğrulanamadı"));
            }
        }
    }
}
