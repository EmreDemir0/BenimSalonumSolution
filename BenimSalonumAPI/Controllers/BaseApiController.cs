using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace BenimSalonumAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        protected readonly IDistributedCache Cache;
        protected readonly ILogger Logger;

        protected BaseApiController(IDistributedCache cache, ILogger logger)
        {
            Cache = cache;
            Logger = logger;
        }

        protected async Task<T> GetCachedDataAsync<T>(string key, Func<Task<T>> getData, TimeSpan? expiration = null)
        {
            var cachedData = await Cache.GetStringAsync(key);
            
            if (!string.IsNullOrEmpty(cachedData))
            {
                return JsonSerializer.Deserialize<T>(cachedData);
            }

            var data = await getData();
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(10)
            };

            await Cache.SetStringAsync(
                key, 
                JsonSerializer.Serialize(data),
                options);

            return data;
        }

        protected IActionResult CreateReport<T>(T data, string reportType)
        {
            try
            {
                Logger.LogInformation($"Rapor oluşturuluyor: {reportType}");

                // Excel, PDF veya diğer formatlarda rapor oluşturma işlemleri burada yapılacak
                // Şimdilik JSON dönüyoruz
                return Ok(new 
                { 
                    Success = true,
                    ReportType = reportType,
                    Data = data,
                    GeneratedAt = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Rapor oluşturulurken hata: {reportType}");
                return StatusCode(500, new { Error = "Rapor oluşturulurken bir hata oluştu" });
            }
        }
    }
}
