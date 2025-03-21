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

            // **Tüm önceki tokenları iptal et**
            await _refreshTokenRepo.RevokeUserRefreshTokens(user.Id.ToString());
            await _refreshTokenRepo.RevokeUserAccessTokens(user.Id);

            // **Yeni Access Token oluştur**
            var accessToken = await _jwtTokenService.GenerateToken(user.Id);

            // **Yeni Refresh Token oluştur**
            var refreshToken = _tokenService.GenerateRefreshToken(user.Id.ToString());
            await _refreshTokenRepo.SaveRefreshToken(refreshToken);

            Console.WriteLine($"✅ Kullanıcı {user.KullaniciAdi} başarıyla giriş yaptı.");

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

            if (existingToken == null || existingToken.IsRevoked || existingToken.Expires < DateTime.UtcNow) // ✅ UTC kullanıyoruz
            {
                return Unauthorized("Geçersiz veya süresi dolmuş refresh token.");
            }

            // **Eski tokenları iptal et**
            await _refreshTokenRepo.RevokeUserRefreshTokens(existingToken.UserId);

            // **Yeni Refresh Token oluştur ve kaydet**
            var newRefreshToken = _tokenService.GenerateRefreshToken(existingToken.UserId);
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
    }
}
