using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace BenimSalonum.Tools
{
    public static class EncryptDatabasePassword
    {
        public static void EncryptAndUpdateAppSettings(string plainPassword)
        {
            if (string.IsNullOrEmpty(plainPassword))
            {
                throw new ArgumentException("Şifre boş olamaz.", nameof(plainPassword));
            }

            try
            {
                // ✅ Güncellenecek JSON dosyalarını belirle
                string consoleConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
                string apiConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\BenimSalonumAPI\appsettings.json");

                Console.WriteLine($"📂 Güncellenen Console JSON: {consoleConfigPath}");
                Console.WriteLine($"📂 Güncellenen API JSON: {Path.GetFullPath(apiConfigPath)}");

                // ✅ JSON dosyalarını güncelle
                UpdateJson(consoleConfigPath, plainPassword);
                UpdateJson(apiConfigPath, plainPassword);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ HATA: JSON dosyası güncellenemedi! {ex.Message}");
            }
        }

        private static void UpdateJson(string filePath, string plainPassword)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"❌ UYARI: {filePath} bulunamadı!");
                return;
            }

            try
            {
                // 📌 JSON dosyasını oku
                string json = File.ReadAllText(filePath);
                JObject jsonObj = JObject.Parse(json);

                // 📌 Şifreyi şifrele
                string? encryptedPassword = AesEncryption.Encrypt(plainPassword);

                // Null kontrolü ekleyerek şifrenin boş olup olmadığını kontrol edelim
                if (encryptedPassword == null)
                {
                    throw new InvalidOperationException("Şifreleme işlemi başarısız oldu, şifre null döndü.");
                }

                Console.WriteLine($"🔒 Şifrelenmiş Şifre: {encryptedPassword}");

                Console.WriteLine($"🔒 Şifrelenmiş Şifre: {encryptedPassword}");

                // 📌 JSON içeriğini güncelle
                jsonObj["DatabaseSettings"]["Password"] = $"ENC({encryptedPassword})";

                // 📌 Güncellenmiş JSON'u dosyaya yaz
                File.WriteAllText(filePath, jsonObj.ToString());

                Console.WriteLine($"✅ JSON dosyası başarıyla güncellendi: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ HATA: {filePath} güncellenemedi! {ex.Message}");
            }
        }
    }
}
