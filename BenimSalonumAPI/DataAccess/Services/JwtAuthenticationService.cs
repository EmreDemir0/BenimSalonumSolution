using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.Threading.Tasks;

namespace BenimSalonumAPI.DataAccess.Services
{
    public static class JwtAuthenticationService
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            try
            {
                Console.WriteLine("✅ JWT Authentication middleware yükleniyor...");

                // 🔹 **Secret Key Doğrulama**
                var secretKey = configuration["JwtSettings:Secret"];
                if (string.IsNullOrWhiteSpace(secretKey))
                {
                    throw new ArgumentNullException(nameof(secretKey), "❌ JWT Secret Key ayarlanmamış! Lütfen appsettings.json içine ekleyin.");
                }
                Console.WriteLine("🔑 JWT Secret Key başarıyla alındı.");

                var key = Encoding.UTF8.GetBytes(secretKey);

                // 🔹 **Authentication Middleware**
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key), // ✅ DOĞRU SECRET KULLANIMI
                        ValidateIssuer = true,
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = configuration["JwtSettings:Audience"],
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero, // ⏳ **Token süre farkını sıfırladık**
                        CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false } // 🛠 **Önbellek temizlendi**
                    };

                    // 🔹 **Token Doğrulama ve Loglama**
                    options.Events = new JwtBearerEvents
                    {
                        // **Authorization Header Loglama**
                        OnMessageReceived = context =>
                        {
                            var authHeader = context.Request.Headers["Authorization"].ToString();
                            if (!string.IsNullOrEmpty(authHeader))
                            {
                                Console.WriteLine("✅ API'ye Authorization Header Geldi!");
                                Console.WriteLine($"🔹 Authorization Header: {authHeader}");

                                var tokenParts = authHeader.Split(' ');
                                if (tokenParts.Length == 2 && tokenParts[0] == "Bearer")
                                {
                                    string token = tokenParts[1];
                                    Console.WriteLine($"📌 Çıkarılan Token: {token}");
                                    context.Token = token;

                                    // **Token Format Kontrolü**
                                    var tokenSegments = token.Split('.');
                                    if (tokenSegments.Length == 3)
                                    {
                                        Console.WriteLine("✅ Token formatı doğru!");
                                    }
                                    else
                                    {
                                        Console.WriteLine("❌ HATALI TOKEN FORMAT! JWT üç parçadan oluşmalıdır: Header.Payload.Signature");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("❌ Token formatı 'Bearer {token}' şeklinde değil!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("❌ API'ye Authorization Header ulaşmadı!");
                            }

                            return Task.CompletedTask;
                        },

                        // **Token Geçerlilik Kontrolü**
                        OnTokenValidated = context =>
                        {
                            var expClaim = context.Principal?.FindFirst("exp")?.Value;
                            if (!string.IsNullOrEmpty(expClaim) && long.TryParse(expClaim, out long expTimestamp))
                            {
                                var expUtc = DateTimeOffset.FromUnixTimeSeconds(expTimestamp).UtcDateTime;
                                var expLocal = expUtc.ToLocalTime(); // ⏳ **Sunucuya göre yerel saate çevir**

                                Console.WriteLine($"✅ Token UTC Süresi: {expUtc}");
                                Console.WriteLine($"✅ Token Yerel Süresi: {expLocal}");
                                Console.WriteLine($"🕒 Şu Anki UTC Zamanı: {DateTime.UtcNow}");
                                Console.WriteLine($"🕒 Şu Anki Yerel Zaman: {DateTime.Now}");
                            }
                            else
                            {
                                Console.WriteLine("❌ Token içinde 'exp' (expiration) claim'i bulunamadı veya geçersiz!");
                            }

                            return Task.CompletedTask;
                        },

                        // **Hata Yönetimi**
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine("❌ JWT Authentication Başarısız!");
                            Console.WriteLine($"🔴 Hata Mesajı: {context.Exception.Message}");
                            Console.WriteLine($"🔴 Hata Tipi: {context.Exception.GetType().Name}");

                            if (context.Exception is SecurityTokenExpiredException expiredException)
                            {
                                Console.WriteLine($"⚠️ Token süresi dolmuş! Exp: {expiredException.Expires}");
                            }
                            else if (context.Exception is SecurityTokenInvalidSignatureException)
                            {
                                Console.WriteLine("⚠️ Token imzası doğrulanamadı. Secret Key yanlış olabilir.");
                            }
                            else if (context.Exception is SecurityTokenMalformedException)
                            {
                                Console.WriteLine("⚠️ Token biçimi hatalı. Token bozulmuş olabilir!");
                            }
                            else if (context.Exception is SecurityTokenNotYetValidException)
                            {
                                Console.WriteLine("⚠️ Token henüz geçerli değil!");
                            }
                            else if (context.Exception is ArgumentException)
                            {
                                Console.WriteLine("⚠️ Token yanlış karakter içeriyor olabilir!");
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

                Console.WriteLine("✅ JWT Authentication middleware başarıyla yüklendi.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ JWT Authentication middleware yüklenirken hata oluştu: {ex.Message}");
                throw;
            }
        }
    }
}
