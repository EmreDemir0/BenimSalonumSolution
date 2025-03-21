using BenimSalonumAPI.DataAccess.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace BenimSalonum.API.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BenimSalonumContext>(); // Veritabanı context'ini al

                try
                {
                    Console.WriteLine("📌 Veritabanı güncelleniyor...");

                    // 📌 **Eğer tablo yoksa, migrasyonu uygula**
                    if (!dbContext.Database.GetPendingMigrations().Any())
                    {
                        Console.WriteLine("✅ Mevcut migrasyonlar uygulanmış, tablo güncelleniyor...");
                    }
                    else
                    {
                        dbContext.Database.Migrate();
                        Console.WriteLine("✅ Yeni migrasyonlar uygulandı.");
                    }    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Veritabanı güncelleme hatası: {ex.Message}");
                }
            }
            return host;
        }
    }
}
