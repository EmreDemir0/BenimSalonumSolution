using System;
using System.IO;
using BenimSalonum.Tools;

namespace SetDatabasePassword
{
    class Program
    {
        static void Main(string[] args)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            string encryptedPassword = "HfetdUomva8MVAGpa/eFpH4C7wVjRDKR0WulT+jhdP8j1voJCmEPqoifFKwg1ofO";
            static bool IsBase64String(string base64)
            {
                Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
                return Convert.TryFromBase64String(base64, buffer, out _);
            }
            if (IsBase64String(encryptedPassword))
            {
                Console.WriteLine("Şifre Base64 formatında geçerli.");
            }
            else
            {
                Console.WriteLine("[HATA] Şifre Base64 formatında değil!");
            }





            Console.WriteLine($"Çalışma Dizini: {basePath}");

            Console.WriteLine("⚡ Kullanılan appsettings.json dosyası:");
            Console.WriteLine($"📂 {Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json")}");


            string configPath = Path.Combine(basePath, "appsettings.json");
            if (!File.Exists(configPath))
            {
                Console.WriteLine("UYARI: appsettings.json bulunamadı! Lütfen dosyanın doğru dizinde olduğundan emin olun.");
                Console.ReadLine(); // 🚀 Kapatmayı önlemek için
                return;
            }

            string configPath2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
            Console.WriteLine($"🔍 Güncellenmesi gereken JSON dosyası: {configPath2}");


            //Console.Write("📌 **Şifrelenmiş veriyi girin:** ");
            //string encryptedPassword = Console.ReadLine();

            //string decryptedPassword = AesEncryption.Decrypt(encryptedPassword);
            //Console.WriteLine($"✅ Çözülen Şifre: {decryptedPassword}");
            try
            {
                Console.Write("📌 **Veritabanı şifrenizi girin**: "); // ✅ Şifre istiyoruz
                string plainPassword = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(plainPassword))
                {
                    Console.WriteLine("⚠️ HATA: Şifre boş olamaz!");
                    Console.ReadLine();
                    return;
                }

                EncryptDatabasePassword.EncryptAndUpdateAppSettings(plainPassword);
                Console.WriteLine("✅ Şifre başarıyla kaydedildi!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ HATA: " + ex.Message);
            }

        //    /////
        //    string apiConfigPath = @"C:\Projects\BenimSalonumSolution\BenimSalonumAPI\appsettings.json";
        //    string consoleConfigPath = AppDomain.CurrentDomain.BaseDirectory + "appsettings.json";

        //    Console.WriteLine($"[DEBUG] API İçin JSON Dosyası: {apiConfigPath}");
        //    Console.WriteLine($"[DEBUG] Console İçin JSON Dosyası: {consoleConfigPath}");

        //    // **API ve Console içindeki JSON içeriğini oku ve karşılaştır**
        //    Console.WriteLine("\n*** API İçin appsettings.json İçeriği ***");
        //    ReadJson(apiConfigPath);

        //    Console.WriteLine("\n*** Console İçin appsettings.json İçeriği ***");
        //    ReadJson(consoleConfigPath);
        

        //static void ReadJson(string filePath)
        //{
        //    if (!File.Exists(filePath))
        //    {
        //        Console.WriteLine($"[HATA] JSON dosyası bulunamadı: {filePath}");
        //        return;
        //    }

        //    string jsonContent = File.ReadAllText(filePath);
        //    Console.WriteLine(jsonContent);
        //}


        //////



        Console.WriteLine("🔄 Programı kapatmak için herhangi bir tuşa basın...");
            Console.ReadLine(); // 🚀 Kapatmayı önlemek için
        }
    }
}
