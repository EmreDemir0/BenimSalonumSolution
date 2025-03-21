using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using BenimSalonumAPI.DataAccess.Services;
using BenimSalonum.Entities.Tables;
using BenimSalonumAPI.DataAccess.Context;
using BenimSalonumAPI.DataAccess.Repositories;
using System.Security.Claims;
using System.Threading.Tasks;

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

        public AuthController(JwtTokenService jwtTokenService, BenimSalonumContext context, TokenService tokenService, RefreshTokenRepository refreshTokenRepo)
        {
            _jwtTokenService = jwtTokenService;
            _context = context;
            _tokenService = tokenService;
            _refreshTokenRepo = refreshTokenRepo;
        }

        [HttpGet("test")]
        public IActionResult TestEndpoint()
        {
            return Ok(new { message = "API çalışıyor!" });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] KullaniciTable userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Gönderilen kullanıcı verisi hatalı.");
            }

            var user = await _context.Kullanicilar
                .FirstOrDefaultAsync(x => x.KullaniciAdi == userDto.KullaniciAdi);

            if (user == null || user.Parola != userDto.Parola)
            {
                return Unauthorized("Geçersiz kullanıcı adı veya şifre.");
            }

            // ✅ Tüm önceki tokenları iptal et
            await _refreshTokenRepo.RevokeUserRefreshTokens(user.Id.ToString());
            await _refreshTokenRepo.RevokeUserAccessTokens(user.Id);

            // ✅ Yeni Access Token oluştur
            var accessToken = await _jwtTokenService.GenerateToken(user.Id);

            // ✅ Kullanıcının cihaz ve bağlantı bilgilerini al
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Bilinmiyor";
            var userAgent = Request.Headers["User-Agent"].ToString();
            var deviceName = Request.Headers["X-Device-Name"].ToString() ?? "Bilinmiyor cihaz";
            var platform = Request.Headers["X-Platform"].ToString() ?? "Bilinmiyor platform";

            // ✅ Yeni Refresh Token oluştur (IP, cihaz ve platform bilgileri ile)
            var refreshToken = _tokenService.GenerateRefreshToken(
                user.Id.ToString(),
                ipAddress,
                userAgent,
                deviceName,
                platform
            );

            await _refreshTokenRepo.SaveRefreshToken(refreshToken);

            Console.WriteLine($"✅ Kullanıcı {user.KullaniciAdi} giriş yaptı. IP: {ipAddress} | Cihaz: {deviceName} | Platform: {platform}");

            return Ok(new
            {
                accessToken,
                refreshToken = refreshToken.Token,
                role = user.Gorevi
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

            // ❗ Yeni Refresh Token oluşturmak için cihaz bilgilerini yeniden al
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Bilinmiyor";
            var userAgent = Request.Headers["User-Agent"].ToString();
            var deviceName = Request.Headers["X-Device-Name"].ToString() ?? "Bilinmiyor cihaz";
            var platform = Request.Headers["X-Platform"].ToString() ?? "Bilinmiyor platform";

            // ✅ Yeni refresh token üret (tüm bilgileri geçir)
            var newRefreshToken = _tokenService.GenerateRefreshToken(
                existingToken.UserId,
                ip,
                userAgent,
                deviceName,
                platform
            );

            await _refreshTokenRepo.RevokeUserRefreshTokens(existingToken.UserId); // eskisini iptal et
            await _refreshTokenRepo.SaveRefreshToken(newRefreshToken);

            Console.WriteLine($"✅ Yeni Refresh Token oluşturuldu: {newRefreshToken.Token}");

            return Ok(new
            {
                refreshToken = newRefreshToken.Token
            });
        }


        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Console.WriteLine("📌 Kullanıcı ID'si: " + (userId ?? "NULL GELİYOR!"));

            if (string.IsNullOrEmpty(userId))
            {
                Console.WriteLine("❌ Kullanıcı ID bulunamadı! Token API'ye ulaşıyor mu?");
                return Unauthorized("Kimlik doğrulama başarısız! Lütfen Access Token’ı kontrol et.");
            }

            Console.WriteLine($"✅ Kullanıcı {userId} başarıyla doğrulandı.");

            // **Tüm tokenları iptal et**
            await _refreshTokenRepo.RevokeUserRefreshTokens(userId);
            await _refreshTokenRepo.RevokeUserAccessTokens(int.Parse(userId));

            Console.WriteLine("✅ Logout işlemi tamamlandı, tüm tokenlar iptal edildi.");
            return Ok("Çıkış yapıldı, tüm tokenlar geçersiz hale getirildi.");
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

        // 📌 Kullanıcının refresh token ile çıkış yapabilmesini sağlayan endpoint
        [HttpPost("logout-with-refresh")]
        public async Task<IActionResult> LogoutWithRefresh([FromBody] RefreshTokenRequest request)
        {
            // 🔐 Token boş gönderilirse reddet
            if (string.IsNullOrEmpty(request.RefreshToken))
                return BadRequest("Refresh token boş olamaz.");

            // 🔎 Veritabanında ilgili tokenı ara
            var refreshToken = await _refreshTokenRepo.GetRefreshToken(request.RefreshToken);

            // ⛔ Token bulunamadıysa, iptal edildiyse ya da süresi dolduysa → 401 döndür
            if (refreshToken == null || refreshToken.IsRevoked || refreshToken.Expires < DateTime.UtcNow)
            {
                Console.WriteLine("❌ Refresh token geçersiz veya süresi dolmuş.");
                return Unauthorized("Geçersiz veya süresi dolmuş refresh token.");
            }

            // ✅ Geçerli refresh token ile kullanıcı ID'si al
            var userId = refreshToken.UserId;

            // 🔒 Tüm refresh ve access tokenları iptal et
            await _refreshTokenRepo.RevokeUserRefreshTokens(userId);
            await _refreshTokenRepo.RevokeUserAccessTokens(int.Parse(userId));

            Console.WriteLine($"✅ Kullanıcı ({userId}) refresh token ile çıkış yaptı.");
            return Ok("Çıkış yapıldı (refresh token ile).");
        }

        // 🔄 Akıllı logout endpoint'i (Access token varsa onunla, yoksa Refresh Token ile çıkış)
        [HttpPost("smart-logout")]
        public async Task<IActionResult> SmartLogout([FromBody] RefreshTokenRequest request)
        {
            var userIdFromAccessToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(userIdFromAccessToken))
            {
                // ✅ Access token hâlâ geçerli → klasik çıkış
                await _refreshTokenRepo.RevokeUserRefreshTokens(userIdFromAccessToken);
                await _refreshTokenRepo.RevokeUserAccessTokens(int.Parse(userIdFromAccessToken));

                Console.WriteLine($"✅ SmartLogout: Kullanıcı ({userIdFromAccessToken}) access token ile çıkış yaptı.");

                return Ok(new
                {
                    message = "Çıkış yapıldı (access token geçerliydi).",
                    shouldClearStorage = true
                });
            }

            // ⛔ Access token geçersiz → Refresh token gerekli
            if (string.IsNullOrEmpty(request?.RefreshToken))
            {
                Console.WriteLine("❌ SmartLogout: Refresh token body'de eksik.");
                return BadRequest(new
                {
                    message = "Refresh token gerekli (access token geçersiz).",
                    shouldClearStorage = false
                });
            }

            var refreshToken = await _refreshTokenRepo.GetRefreshToken(request.RefreshToken);

            if (refreshToken == null || refreshToken.IsRevoked || refreshToken.Expires < DateTime.UtcNow)
            {
                Console.WriteLine("❌ SmartLogout: Refresh token geçersiz veya süresi dolmuş.");
                return Unauthorized(new
                {
                    message = "Geçersiz veya süresi dolmuş refresh token.",
                    shouldClearStorage = true // yine de localStorage silinmeli
                });
            }

            // ✅ Refresh token geçerli → tokenları iptal et
            await _refreshTokenRepo.RevokeUserRefreshTokens(refreshToken.UserId);
            await _refreshTokenRepo.RevokeUserAccessTokens(int.Parse(refreshToken.UserId));

            Console.WriteLine($"✅ SmartLogout: Kullanıcı ({refreshToken.UserId}) refresh token ile çıkış yaptı.");

            return Ok(new
            {
                message = "Çıkış yapıldı (refresh token ile).",
                shouldClearStorage = true
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

            // ✅ Kullanıcının aktif refresh token'larını getir
            var devices = await _context.RefreshTokens
                .Where(x => x.UserId == userId && !x.IsRevoked && x.Expires > DateTime.UtcNow)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            // 🔍 Listeyi sadeleştirip sadece kullanıcıya faydalı alanları gönderiyoruz
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

            Console.WriteLine($"✅ Token {token.Id} iptal edildi (cihazdan çıkış yapıldı).");
            return Ok("Cihazdan çıkış yapıldı.");
        }





    }
}
