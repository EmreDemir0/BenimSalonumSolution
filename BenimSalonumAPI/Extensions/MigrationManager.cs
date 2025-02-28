using BenimSalonumAPI.DataAccess.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

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
                    Console.WriteLine("Checking for database migrations...");
                    dbContext.Database.Migrate(); // Migrasyonları uygula
                    Console.WriteLine("Database migration applied successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Database migration failed: {ex.Message}");
                }
            }
            return host;
        }
    }
}
