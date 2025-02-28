using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace BenimSalonum.Tools
{
    public static class SettingsTool
    {
        public static string GetDatabasePassword()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string encryptedPassword = config["DatabaseSettings:Password"];

            if (string.IsNullOrEmpty(encryptedPassword))
            {
                throw new InvalidOperationException("Veritabanı şifresi appsettings.json içinde bulunamadı.");
            }

            return encryptedPassword.StartsWith("ENC(") ? AesEncryption.Decrypt(encryptedPassword.Replace("ENC(", "").Replace(")", "")) : encryptedPassword;
        }

        public static string GetConnectionString()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string connectionString = config.GetConnectionString("DefaultConnection");
            string decryptedPassword = GetDatabasePassword();

            return connectionString.Replace("ENC(U2hpcmVsZW5taXNzaWJsaWdlZm9yZW50aGV1c2Vy)", decryptedPassword);
        }
    }
}
