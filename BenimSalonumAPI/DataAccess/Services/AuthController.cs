using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using BenimSalonumAPI.DataAccess.Services;
using BenimSalonum.Entities.Tables;
using BenimSalonumAPI.DataAccess.Context;
using BenimSalonumAPI.DataAccess.Repositories;
using System.Security.Claims;
using BenimSalonumAPI.Models;
using System.Threading.Tasks;
using BenimSalonumAPI.Services;
using System;
using System.Linq;
using BenimSalonum.Entities.DTOs;

namespace BenimSalonumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenService _jwtTokenService;
        private readonly BenimSalonumContext _context;
        private readonly TokenService _tokenService;
        private readonly RefreshTokenRepository _refreshTokenRepo;
        private readonly ILogService _logService;

        public AuthController(
            JwtTokenService jwtTokenService, 
            BenimSalonumContext context, 
            TokenService tokenService, 
            RefreshTokenRepository refreshTokenRepo,
            ILogService logService)
        {
            _jwtTokenService = jwtTokenService;
            _context = context;
            _tokenService = tokenService;
            _refreshTokenRepo = refreshTokenRepo;
            _logService = logService;
        }

        [HttpGet("test")]
        public IActionResult TestEndpoint()
        {
            return Ok(new { message = "API çalışıyor!" });
        }

        // DTO sınıfı (istersen ayrı dosyada tutabilirsin)
        public class LoginRequest
        {
            public string KullaniciAdi { get; set; }
            public string Parola { get; set; }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest userDto)
        {
            if (userDto == null || string.IsNullOrEmpty(userDto.KullaniciAdi) || string.IsNullOrEmpty(userDto.Parola))
            {
                await _logService.LogWarningAsync("Giriş başarısız: Kullanıcı adı veya şifre boş", "AuthController");
                return BadRequest("Kullanıcı adı ve şifre zorunludur.");
            }

            var user = await _context.Kullanicilar
                .FirstOrDefaultAsync(x => x.KullaniciAdi == userDto.KullaniciAdi);

            // Cihaz bilgileri
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Bilinmiyor";
            var userAgent = Request.Headers["User-Agent"].ToString();
            var deviceName = Request.Headers["X-Device-Name"].ToString() ?? "Bilinmiyor cihaz";
            var platform = Request.Headers["X-Platform"].ToString() ?? "Bilinmiyor platform";

            if (user == null)
            {
                await _logService.LogKullaniciGirisAsync(userDto.KullaniciAdi, false, ipAddress);
                await _logService.LogWarningAsync(
                    $"Kullanıcı bulunamadı: {userDto.KullaniciAdi}", 
                    "AuthController", 
                    LogVisibility.AdminOnly, 
                    new { IpAddress = ipAddress, DeviceName = deviceName, Platform = platform }
                );

                return Unauthorized("Geçersiz kullanıcı adı veya şifre.");
            }

            if (user.Parola != userDto.Parola)
            {
                await _logService.LogKullaniciGirisAsync(userDto.KullaniciAdi, false, ipAddress);
                await _logService.LogWarningAsync(
                    $"Parola uyuşmuyor: {userDto.KullaniciAdi}", 
                    "AuthController", 
                    LogVisibility.AdminOnly,
                    new { IpAddress = ipAddress, DeviceName = deviceName, Platform = platform }
                );

                return Unauthorized("Geçersiz kullanıcı adı veya şifre.");
            }

            // Aktif oturumları kontrol et (otomatik olarak iptal etmeden önce)
            var existingSessions = await _context.RefreshTokens
                .Where(r => r.UserId == user.Id.ToString() && r.IsRevoked == false && 
                      !(r.DeviceName == deviceName && r.Platform == platform && r.UserAgent == userAgent))
                .Select(r => new {
                    r.Id,
                    r.DeviceName,
                    r.Platform,
                    r.CreatedAt,
                    r.IpAddress
                })
                .ToListAsync();
                
            // Yeni Access Token ve Refresh Token üret
            var accessToken = await _jwtTokenService.GenerateToken(user.Id);
            
            // Refresh token'ı doğru parametrelerle oluştur
            var refreshTokenObj = _tokenService.GenerateRefreshToken(user.Id.ToString(), ipAddress, userAgent, deviceName, platform);
            var refreshToken = refreshTokenObj.Token;
            
            // Kullanıcıya mevcut oturumlar hakkında bilgi ver
            if (existingSessions.Any())
            {
                return Ok(new { 
                    hasActiveSessions = true,
                    activeSessions = existingSessions,
                    pendingToken = accessToken,
                    pendingRefreshToken = refreshToken,
                    userId = user.Id,
                    message = "Aktif oturumlarınız var. Nasıl devam etmek istersiniz?"
                });
            }

            // Aynı cihazdaki önceki tokenları iptal et
            await _refreshTokenRepo.RevokeDuplicateDeviceTokens(user.Id.ToString(), deviceName, platform, userAgent);

            // Yeni token'ı kaydet ve kullanıcıya dön
            await _refreshTokenRepo.SaveRefreshToken(refreshTokenObj);

            // Kullanıcının son giriş tarihini güncelle 
            user.SonGirisTarihi = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            await _logService.LogKullaniciGirisAsync(userDto.KullaniciAdi, true, ipAddress);
            await _logService.LogInformationAsync($"Kullanıcı başarıyla giriş yaptı: {userDto.KullaniciAdi}", "AuthController");

            return Ok(new
            {
                accessToken,
                refreshToken,
                hasActiveSessions = false,
                message = "Başarıyla giriş yapıldı"
            });
        }

        public class RefreshTokenRequest
        {
            public string RefreshToken { get; set; }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (string.IsNullOrEmpty(request.RefreshToken))
            {
                return BadRequest("Refresh Token boş olamaz.");
            }

            var existingToken = await _refreshTokenRepo.GetRefreshToken(request.RefreshToken);

            if (existingToken == null || existingToken.IsRevoked || existingToken.Expires < DateTime.UtcNow)
            {
                return Unauthorized("Geçersiz veya süresi dolmuş refresh token.");
            }

            // Yeni Refresh Token oluşturmak için cihaz bilgilerini yeniden al
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Bilinmiyor";
            var userAgent = Request.Headers["User-Agent"].ToString();
            var deviceName = Request.Headers["X-Device-Name"].ToString() ?? "Bilinmiyor cihaz";
            var platform = Request.Headers["X-Platform"].ToString() ?? "Bilinmiyor platform";

            // Yeni refresh token üret (tüm bilgileri geçir)
            var newRefreshToken = _tokenService.GenerateRefreshToken(
                existingToken.UserId,
                ip,
                userAgent,
                deviceName,
                platform
            );

            await _refreshTokenRepo.RevokeUserRefreshTokens(existingToken.UserId); // eskisini iptal et
            await _refreshTokenRepo.SaveRefreshToken(newRefreshToken);

            return Ok(new
            {
                refreshToken = newRefreshToken.Token
            });
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            // Kullanıcı ID'sini al
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var kullaniciAdi = User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                await _logService.LogWarningAsync("Çıkış yaparken kimlik doğrulama başarısız", "AuthController");
                return Unauthorized("Kimlik doğrulama başarısız! Lütfen Access Token'ı kontrol et.");
            }

            // Cihaz bilgilerini al
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Bilinmiyor";
            var deviceName = Request.Headers["X-Device-Name"].ToString() ?? "Bilinmiyor cihaz";
            var platform = Request.Headers["X-Platform"].ToString() ?? "Bilinmiyor platform";

            // Tüm tokenları iptal et
            await _refreshTokenRepo.RevokeUserRefreshTokens(userId);
            await _refreshTokenRepo.RevokeUserAccessTokens(int.Parse(userId));
            
            // Çıkış işlemini logla
            if (!string.IsNullOrEmpty(kullaniciAdi))
            {
                await _logService.LogKullaniciCikisAsync(kullaniciAdi);
                await _logService.LogInformationAsync(
                    $"Kullanıcı çıkış yaptı: {kullaniciAdi}", 
                    "AuthController", 
                    LogVisibility.All,
                    new { IpAddress = ipAddress, DeviceName = deviceName, Platform = platform }
                );
            }

            return Ok("Çıkış yapıldı, tüm tokenlar geçersiz hale getirildi.");
        }

        [HttpPost("logout-with-refresh")]
        public async Task<IActionResult> LogoutWithRefresh([FromBody] RefreshTokenRequest request)
        {
            // Token boş gönderilirse reddet
            if (string.IsNullOrEmpty(request.RefreshToken))
                return BadRequest("Refresh token boş olamaz.");

            // Veritabanında ilgili tokenı ara
            var refreshToken = await _refreshTokenRepo.GetRefreshToken(request.RefreshToken);

            // Token bulunamadıysa, iptal edildiyse ya da süresi dolduysa → 401 döndür
            if (refreshToken == null || refreshToken.IsRevoked || refreshToken.Expires < DateTime.UtcNow)
            {
                return Unauthorized("Geçersiz veya süresi dolmuş refresh token.");
            }

            // Cihaz bilgilerini al
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Bilinmiyor";
            var deviceName = Request.Headers["X-Device-Name"].ToString() ?? "Bilinmiyor cihaz";
            var platform = Request.Headers["X-Platform"].ToString() ?? "Bilinmiyor platform";

            // Geçerli refresh token ile kullanıcı ID'si al
            var userId = refreshToken.UserId;

            // Kullanıcı bilgisini al
            var user = await _context.Kullanicilar.FirstOrDefaultAsync(u => u.Id == int.Parse(userId));
            var kullaniciAdi = user?.KullaniciAdi ?? "Bilinmiyor";

            // Tüm refresh ve access tokenları iptal et
            await _refreshTokenRepo.RevokeUserRefreshTokens(userId);
            await _refreshTokenRepo.RevokeUserAccessTokens(int.Parse(userId));
            
            return Ok("Çıkış yapıldı (refresh token ile).");
        }

        // Akıllı logout endpoint'i (Access token varsa onunla, yoksa Refresh Token ile çıkış)
        [HttpPost("smart-logout")]
        public async Task<IActionResult> SmartLogout([FromBody] RefreshTokenRequest request)
        {
            var userIdFromAccessToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var kullaniciAdi = User.FindFirst(ClaimTypes.Name)?.Value;
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Bilinmiyor";
            var deviceName = Request.Headers["X-Device-Name"].ToString() ?? "Bilinmiyor cihaz";
            var platform = Request.Headers["X-Platform"].ToString() ?? "Bilinmiyor platform";

            if (!string.IsNullOrEmpty(userIdFromAccessToken))
            {
                // Access token hâlâ geçerli → klasik çıkış
                await _refreshTokenRepo.RevokeUserRefreshTokens(userIdFromAccessToken);
                await _refreshTokenRepo.RevokeUserAccessTokens(int.Parse(userIdFromAccessToken));

                return Ok(new
                {
                    message = "Çıkış yapıldı (access token ile)."
                });
            }

            // Access token geçersiz → Refresh token gerekli
            if (string.IsNullOrEmpty(request?.RefreshToken))
            {
                return BadRequest(new
                {
                    message = "Refresh token gerekli (access token geçersiz).",
                    needsRefreshToken = true
                });
            }

            var refreshToken = await _refreshTokenRepo.GetRefreshToken(request.RefreshToken);

            if (refreshToken == null || refreshToken.IsRevoked || refreshToken.Expires < DateTime.UtcNow)
            {
                return Unauthorized(new
                {
                    message = "Geçersiz veya süresi dolmuş refresh token.",
                    needsLogin = true
                });
            }

            // Kullanıcı bilgisini al
            var user = await _context.Kullanicilar.FirstOrDefaultAsync(u => u.Id == int.Parse(refreshToken.UserId));
            var kullaniciAdiFromToken = user?.KullaniciAdi ?? "Bilinmiyor";

            // Refresh token geçerli → tokenları iptal et
            await _refreshTokenRepo.RevokeUserRefreshTokens(refreshToken.UserId);
            await _refreshTokenRepo.RevokeUserAccessTokens(int.Parse(refreshToken.UserId));

            return Ok(new
            {
                message = "Çıkış yapıldı (refresh token ile)."
            });
        }

        [Authorize]
        [HttpGet("devices")]
        public async Task<IActionResult> GetActiveDevices()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Kullanıcı doğrulanamadı.");
            }

            // Kullanıcının aktif refresh token'larını getir
            var devices = await _context.RefreshTokens
                .Where(x => x.UserId == userId && !x.IsRevoked && x.Expires > DateTime.UtcNow)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            // Listeyi sadeleştirip sadece kullanıcıya faydalı alanları gönderiyoruz
            var response = devices.Select(x => new
            {
                x.Id,
                x.IpAddress,
                x.UserAgent,
                x.DeviceName,
                x.Platform,
                x.CreatedAt,
                x.Expires
            });

            return Ok(response);
        }
        [Authorize]
        [HttpPost("devices/logout/{tokenId}")]
        public async Task<IActionResult> RevokeTokenById(int tokenId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var token = await _context.RefreshTokens.FindAsync(tokenId);
            if (token == null || token.UserId != userId || token.IsRevoked)
            {
                return NotFound("Token bulunamadı veya zaten iptal edilmiş.");
            }

            token.IsRevoked = true;
            await _context.SaveChangesAsync();

            return Ok("Cihazdan çıkış yapıldı.");
        }

        [Authorize]
        [HttpGet("protected")]
        public IActionResult ProtectedEndpoint()
        {
            return Ok("Bu endpoint sadece yetkili kullanıcılar tarafından erişilebilir!");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public IActionResult AdminOnlyEndpoint()
        {
            return Ok("Bu endpoint sadece Admin kullanıcılar tarafından erişilebilir!");
        }

        [HttpPost("logout-other-sessions")]
        [Authorize]
        public async Task<IActionResult> LogoutOtherSessions([FromBody] LogoutOtherSessionsDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "Yetkisiz erişim" });
            
            // Cihaz bilgileri
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Bilinmiyor";
            var userAgent = Request.Headers["User-Agent"].ToString();
            var deviceName = Request.Headers["X-Device-Name"].ToString() ?? "Bilinmiyor cihaz";
            var platform = Request.Headers["X-Platform"].ToString() ?? "Bilinmiyor platform";
            
            // Diğer aktif oturumları kapat
            var existingTokens = await _context.RefreshTokens
                .Where(r => r.UserId == userId && r.IsRevoked == false && 
                      !(r.DeviceName == deviceName && r.Platform == platform && r.UserAgent == userAgent))
                .ToListAsync();
            
            foreach (var token in existingTokens)
            {
                token.IsRevoked = true;
                token.RevokedAt = DateTime.UtcNow;
                token.ReasonRevoked = "Kullanıcı tarafından sonlandırıldı";
            }
            
            // Yeni refresh token'ı kaydet
            var refreshTokenObj = _tokenService.GenerateRefreshToken(userId, ipAddress, userAgent, deviceName, platform);
            refreshTokenObj.Token = dto.NewRefreshToken;
            
            _context.RefreshTokens.Add(refreshTokenObj);
            await _context.SaveChangesAsync();
            
            await _logService.LogInformationAsync($"Diğer oturumlar kapatıldı. Kullanıcı ID: {userId}", "AuthController");
            
            return Ok(new { message = "Diğer oturumlar kapatıldı ve yeni oturum başlatıldı" });
        }

        [HttpPost("keep-all-sessions")]
        [Authorize]
        public async Task<IActionResult> KeepAllSessions([FromBody] KeepAllSessionsDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "Yetkisiz erişim" });
            
            // Cihaz bilgileri
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Bilinmiyor";
            var userAgent = Request.Headers["User-Agent"].ToString();
            var deviceName = Request.Headers["X-Device-Name"].ToString() ?? "Bilinmiyor cihaz";
            var platform = Request.Headers["X-Platform"].ToString() ?? "Bilinmiyor platform";
            
            // Yeni oturumu ekle (diğerlerini kapatmadan)
            var refreshTokenObj = _tokenService.GenerateRefreshToken(userId, ipAddress, userAgent, deviceName, platform);
            refreshTokenObj.Token = dto.NewRefreshToken;
            
            _context.RefreshTokens.Add(refreshTokenObj);
            await _context.SaveChangesAsync();
            
            await _logService.LogInformationAsync($"Tüm oturumlar aktif bırakıldı. Kullanıcı ID: {userId}", "AuthController");
            
            return Ok(new { message = "Yeni oturum başlatıldı, diğer oturumlar aktif" });
        }
    }
}
