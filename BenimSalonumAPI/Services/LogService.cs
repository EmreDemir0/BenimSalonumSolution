using System;
using System.Threading.Tasks;
using System.Text.Json;
using BenimSalonum.Entities.Tables;
using BenimSalonum.Entities.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Diagnostics;

namespace BenimSalonumAPI.Services
{
    public enum LogSeverity
    {
        Debug = 0,
        Information = 1,
        Warning = 2,
        Error = 3,
        Critical = 4
    }

    public enum LogVisibility
    {
        All = 0,         // Herkes görebilir
        AdminOnly = 1,   // Sadece admin görebilir
        UserOnly = 2     // Sadece ilgili kullanıcı görebilir
    }

    public interface ILogService
    {
        Task LogInformationAsync(string mesaj, string modul = null, LogVisibility visibility = LogVisibility.All, object data = null);
        Task LogWarningAsync(string mesaj, string modul = null, LogVisibility visibility = LogVisibility.All, object data = null);
        Task LogErrorAsync(string mesaj, Exception ex = null, string modul = null, LogVisibility visibility = LogVisibility.All, object data = null);
        Task LogCriticalAsync(string mesaj, Exception ex = null, string modul = null, LogVisibility visibility = LogVisibility.All, object data = null);
        Task LogDebugAsync(string mesaj, string modul = null, LogVisibility visibility = LogVisibility.All, object data = null);
        Task LogKullaniciGirisAsync(string kullaniciAdi, bool basarili, string ipAdresi = null);
        Task LogKullaniciCikisAsync(string kullaniciAdi);
        Task LogKullaniciIslemAsync(string yapilanIslem, object detaylar = null);
        Task LogOnemliIslemAsync(string islemAdi, object islemDetay);
    }

    public class LogService : ILogService
    {
        private readonly IRepository<KullaniciLogTable> _kullaniciLogRepository;
        private readonly IRepository<SistemLogTable> _sistemLogRepository;
        private readonly ILogger<LogService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogService(
            IRepository<KullaniciLogTable> kullaniciLogRepository,
            IRepository<SistemLogTable> sistemLogRepository,
            ILogger<LogService> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _kullaniciLogRepository = kullaniciLogRepository;
            _sistemLogRepository = sistemLogRepository;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task LogInformationAsync(string mesaj, string modul = null, LogVisibility visibility = LogVisibility.All, object data = null)
        {
            await LogSystemAsync(mesaj, LogSeverity.Information, null, modul, visibility, data);
            _logger.LogInformation("{Modul}: {Mesaj}", modul ?? "Sistem", mesaj);
            ConsoleLog(mesaj, "INFO", modul);
        }

        public async Task LogWarningAsync(string mesaj, string modul = null, LogVisibility visibility = LogVisibility.All, object data = null)
        {
            await LogSystemAsync(mesaj, LogSeverity.Warning, null, modul, visibility, data);
            _logger.LogWarning("{Modul}: {Mesaj}", modul ?? "Sistem", mesaj);
            ConsoleLog(mesaj, "WARNING", modul, ConsoleColor.Yellow);
        }

        public async Task LogErrorAsync(string mesaj, Exception ex = null, string modul = null, LogVisibility visibility = LogVisibility.All, object data = null)
        {
            await LogSystemAsync(mesaj, LogSeverity.Error, ex, modul, visibility, data);
            _logger.LogError(ex, "{Modul}: {Mesaj}", modul ?? "Sistem", mesaj);
            ConsoleLog(mesaj + (ex != null ? $" - Hata: {ex.Message}" : ""), "ERROR", modul, ConsoleColor.Red);
        }

        public async Task LogCriticalAsync(string mesaj, Exception ex = null, string modul = null, LogVisibility visibility = LogVisibility.All, object data = null)
        {
            await LogSystemAsync(mesaj, LogSeverity.Critical, ex, modul, visibility, data);
            _logger.LogCritical(ex, "{Modul}: {Mesaj}", modul ?? "Sistem", mesaj);
            ConsoleLog(mesaj + (ex != null ? $" - Hata: {ex.Message}" : ""), "CRITICAL", modul, ConsoleColor.DarkRed);
        }

        public async Task LogDebugAsync(string mesaj, string modul = null, LogVisibility visibility = LogVisibility.All, object data = null)
        {
            await LogSystemAsync(mesaj, LogSeverity.Debug, null, modul, visibility, data);
            _logger.LogDebug("{Modul}: {Mesaj}", modul ?? "Sistem", mesaj);
            
            #if DEBUG
            ConsoleLog(mesaj, "DEBUG", modul, ConsoleColor.Cyan);
            #endif
        }

        public async Task LogKullaniciGirisAsync(string kullaniciAdi, bool basarili, string ipAdresi = null)
        {
            var mesaj = basarili ? "Kullanıcı girişi başarılı" : "Kullanıcı girişi başarısız";
            
            await LogKullaniciIslemAsync(mesaj, new 
            { 
                KullaniciAdi = kullaniciAdi,
                Basarili = basarili,
                IpAdresi = ipAdresi ?? _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "Bilinmiyor"
            });
            
            ConsoleLog($"{kullaniciAdi} kullanıcısı {(basarili ? "başarıyla giriş yaptı" : "giriş yapmayı denedi ama başarısız oldu")}", 
                "KULLANICI", "Kimlik", basarili ? ConsoleColor.Green : ConsoleColor.Red);
        }

        public async Task LogKullaniciCikisAsync(string kullaniciAdi)
        {
            await LogKullaniciIslemAsync("Kullanıcı çıkışı", new { KullaniciAdi = kullaniciAdi });
            ConsoleLog($"{kullaniciAdi} kullanıcısı çıkış yaptı", "KULLANICI", "Kimlik", ConsoleColor.DarkGreen);
        }

        public async Task LogKullaniciIslemAsync(string yapilanIslem, object detaylar = null)
        {
            try
            {
                var kullaniciAdi = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? "Anonim";
                var jsonDetay = detaylar != null ? JsonSerializer.Serialize(detaylar) : null;
                
                var logEntry = new KullaniciLogTable
                {
                    KullaniciAdi = kullaniciAdi,
                    SonGirisTarihi = DateTime.Now,
                    YapilanIslem = yapilanIslem,
                    YapilanIslemTarihi = DateTime.Now,
                    Detay = jsonDetay
                };

                await _kullaniciLogRepository.AddAsync(logEntry);
                await _kullaniciLogRepository.SaveChangesAsync();
                
                ConsoleLog($"{kullaniciAdi}: {yapilanIslem}", "KULLANICI İŞLEM", null, ConsoleColor.Blue);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı işlemi loglanırken hata oluştu: {Mesaj}", ex.Message);
                ConsoleLog($"Kullanıcı işlemi loglanırken hata oluştu: {ex.Message}", "ERROR", "LogService", ConsoleColor.Red);
            }
        }

        public async Task LogOnemliIslemAsync(string islemAdi, object islemDetay)
        {
            // Önemli işlemler hem sistem loglarına hem de kullanıcı loglarına kaydedilir
            // ve sadece admin tarafından görülebilir
            var jsonDetay = islemDetay != null ? JsonSerializer.Serialize(islemDetay) : null;
            
            await LogSystemAsync(
                $"Önemli işlem: {islemAdi}", 
                LogSeverity.Information, 
                null, 
                "ÖnemliİşlemModülü", 
                LogVisibility.AdminOnly, 
                islemDetay);
                
            await LogKullaniciIslemAsync($"Önemli işlem: {islemAdi}", islemDetay);
            
            ConsoleLog($"ÖNEMLİ İŞLEM: {islemAdi}", "ADMIN", null, ConsoleColor.Magenta);
        }

        private async Task LogSystemAsync(
            string mesaj, 
            LogSeverity seviye, 
            Exception ex = null, 
            string modul = null, 
            LogVisibility visibility = LogVisibility.All, 
            object data = null)
        {
            try
            {
                var kullaniciAdi = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? "Sistem";
                var jsonData = data != null ? JsonSerializer.Serialize(data) : null;
                
                var logEntry = new SistemLogTable
                {
                    Mesaj = mesaj,
                    HataSeviyesi = (int)seviye,
                    Modul = modul ?? "Sistem",
                    IstekYolu = _httpContextAccessor.HttpContext?.Request.Path.Value,
                    KullaniciAdi = kullaniciAdi,
                    IpAdresi = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString(),
                    HataDetay = ex?.ToString(),
                    Tarih = DateTime.Now,
                    SubeId = GetCurrentSubeId(),
                    Veri = jsonData,
                    Gorünürlük = (int)visibility
                };

                await _sistemLogRepository.AddAsync(logEntry);
                await _sistemLogRepository.SaveChangesAsync();
            }
            catch (Exception logEx)
            {
                _logger.LogError(logEx, "Sistem logu kaydedilirken hata oluştu: {Mesaj}", logEx.Message);
                ConsoleLog($"Sistem logu kaydedilirken hata oluştu: {logEx.Message}", "ERROR", "LogService", ConsoleColor.Red);
            }
        }

        private int? GetCurrentSubeId()
        {
            if (_httpContextAccessor.HttpContext == null) return null;
            
            var subeIdClaim = _httpContextAccessor.HttpContext.User?.FindFirst("SubeId");
            if (subeIdClaim != null && int.TryParse(subeIdClaim.Value, out int subeId))
            {
                return subeId;
            }
            
            return null;
        }
        
        private void ConsoleLog(string message, string level, string module = null, ConsoleColor color = ConsoleColor.White)
        {
            try
            {
                var originalColor = Console.ForegroundColor;
                Console.ForegroundColor = color;
                
                var timestamp = DateTime.Now.ToString("HH:mm:ss");
                var moduleText = string.IsNullOrEmpty(module) ? "" : $" [{module}]";
                
                Console.WriteLine($"[{timestamp}] [{level}]{moduleText} {message}");
                
                Console.ForegroundColor = originalColor;
            }
            catch
            {
                // Konsol logu yazılamadıysa sessizce devam et
            }
        }
    }
}
