using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BenimSalonumAPI.DataAccess.Services
{
    public class BackupService
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string _backupPath;

        public BackupService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
            _backupPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backups");
            
            if (!Directory.Exists(_backupPath))
                Directory.CreateDirectory(_backupPath);
        }

        public async Task<string> CreateBackupAsync()
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var backupFileName = $"BenimSalonum_Backup_{timestamp}.bak";
            var fullPath = Path.Combine(_backupPath, backupFileName);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                
                var query = $"BACKUP DATABASE BenimSalonum TO DISK = '{fullPath}' WITH FORMAT, MEDIANAME = 'BenimSalonumBackups', NAME = 'Full Backup of BenimSalonum';";
                
                using (var command = new SqlCommand(query, connection))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }

            return fullPath;
        }

        public async Task RestoreBackupAsync(string backupFilePath)
        {
            if (!File.Exists(backupFilePath))
                throw new FileNotFoundException("Yedek dosyası bulunamadı.", backupFilePath);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                
                // Mevcut bağlantıları kapat
                var killConnectionsQuery = @"
                    ALTER DATABASE BenimSalonum SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
                
                var restoreQuery = $@"
                    RESTORE DATABASE BenimSalonum 
                    FROM DISK = '{backupFilePath}' 
                    WITH REPLACE;
                    
                    ALTER DATABASE BenimSalonum SET MULTI_USER;";

                using (var command = new SqlCommand(killConnectionsQuery + restoreQuery, connection))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
