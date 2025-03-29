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

            // Aynı cihazdaki önceki tokenları iptal et
            await _refreshTokenRepo.RevokeDuplicateDeviceTokens(user.Id.ToString(), deviceName, platform, userAgent);

            // Yeni Access Token üret
            var accessToken = await _jwtTokenService.GenerateToken(user.Id);

            // Yeni Refresh Token oluştur ve kaydet
            var refreshToken = _tokenService.GenerateRefreshToken(
                user.Id.ToString(), ipAddress, userAgent, deviceName, platform
            );
            await _refreshTokenRepo.SaveRefreshToken(refreshToken);

            // Kullanıcı girişini logla
            await _logService.LogKullaniciGirisAsync(user.KullaniciAdi, true, ipAddress);
            await _logService.LogInformationAsync(
                $"Kullanıcı başarıyla giriş yaptı: {user.KullaniciAdi}", 
                "AuthController",
                LogVisibility.All,
                new { IpAddress = ipAddress, DeviceName = deviceName, Platform = platform }
            );

            // Rolü null ise "User" olarak ata
            var role = user.Gorevi ?? "User";

            return Ok(new
            {
                KullaniciAdi = user.KullaniciAdi,
                Adi = user.Adi,
                Soyadi = user.Soyadi,
                accessToken,
                refreshToken = refreshToken.Token,
                role // Burada artık null olmayan değeri kullanıyoruz
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
    }
}
