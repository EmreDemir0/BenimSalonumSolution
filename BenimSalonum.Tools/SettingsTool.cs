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
            // appsettings.json dosyasını yükleyip, konfigürasyonu alıyoruz.
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Veritabanı şifresini alıyoruz.
            string? encryptedPassword = config["DatabaseSettings:Password"];

            if (string.IsNullOrEmpty(encryptedPassword))
            {
                throw new InvalidOperationException("Veritabanı şifresi appsettings.json içinde bulunamadı.");
            }

            // Şifreyi şifrelenmişse çözerek geri döndürüyoruz.
            if (encryptedPassword.StartsWith("ENC("))
            {
                string passwordWithoutEnc = encryptedPassword.Replace("ENC(", "").Replace(")", "");
                return AesEncryption.Decrypt(passwordWithoutEnc) ?? throw new InvalidOperationException("Şifre çözülemedi.");
            }

            return encryptedPassword;  // Eğer şifre şifrelenmemişse, olduğu gibi döndür.
        }

        public static string GetConnectionString()
        {
            // appsettings.json dosyasını yükleyip, konfigürasyonu alıyoruz.
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Bağlantı dizesini alıyoruz.
            string? connectionString = config.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Veritabanı bağlantı dizesi appsettings.json içinde bulunamadı.");
            }

            // Şifreyi çözüp bağlantı dizesinde uygun yere yerleştiriyoruz.
            string decryptedPassword = GetDatabasePassword();
            return connectionString.Replace("ENC(YOUR_ENCRYPTED_PASSWORD_HERE)", decryptedPassword);
        }
    }
}
