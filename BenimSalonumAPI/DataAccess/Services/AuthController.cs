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

            // **Access Token oluştur ve kaydet**
            var accessToken = await _jwtTokenService.GenerateToken(user.Id);

            // **Refresh Token oluştur ve kaydet**
            var refreshToken = _tokenService.GenerateRefreshToken(user.Id.ToString());
            await _refreshTokenRepo.SaveRefreshToken(refreshToken);

            return Ok(new
            {
                accessToken,
                refreshToken = refreshToken.Token,
                role = user.Gorevi
            });
        }


        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            var existingToken = await _refreshTokenRepo.GetRefreshToken(refreshToken);
            if (existingToken == null || existingToken.IsRevoked || existingToken.Expires < DateTime.UtcNow)
            {
                return Unauthorized("Geçersiz veya süresi dolmuş refresh token.");
            }

            var newAccessToken = _tokenService.GenerateAccessToken(existingToken.UserId);
            var newRefreshToken = _tokenService.GenerateRefreshToken(existingToken.UserId);

            await _refreshTokenRepo.RevokeRefreshToken(refreshToken);
            await _refreshTokenRepo.SaveRefreshToken(newRefreshToken);

            return Ok(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken.Token
            });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Kullanıcı kimliği bulunamadı.");
            }

            var tokens = await _context.RefreshTokens.Where(t => t.UserId == userId).ToListAsync();
            _context.RefreshTokens.RemoveRange(tokens);
            await _context.SaveChangesAsync();

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
