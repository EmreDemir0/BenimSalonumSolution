using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;

namespace BenimSalonum.Tools
{
    public static class AesEncryption
    {
        private static readonly string AESKey = "ThisIsA32CharLongSecretKey123456"; // 32 karakterlik sabit anahtar
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("ThisIsAnIV123456"); // Sabit IV (16 byte)

        public static string Encrypt(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(AESKey);
                aesAlg.IV = IV; // 🚀 IV sabitleniyor

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public static string Decrypt(string encryptedText)
        {
            try
            {
                if (encryptedText.StartsWith("ENC(") && encryptedText.EndsWith(")"))
                {
                    encryptedText = encryptedText.Substring(4, encryptedText.Length - 5);
                }

                //Console.WriteLine($"[DEBUG] Şifrelenmiş metin: {encryptedText}");

                // **Base64 formatında olup olmadığını kontrol et**
                if (!IsBase64String(encryptedText))
                {
                    Console.WriteLine("[HATA] Base64 şifre çözme başarısız: Base64 formatı geçersiz!");
                    throw new FormatException("Base64 formatı geçersiz: Şifre yanlış kaydedilmiş olabilir.");
                }

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Encoding.UTF8.GetBytes(AESKey);
                    aesAlg.IV = Encoding.UTF8.GetBytes("ThisIsAnIV123456");

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedText)))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                string decryptedText = srDecrypt.ReadToEnd();
                                //Console.WriteLine($"[DEBUG] Çözülen Şifre: {decryptedText}");
                                return decryptedText;
                            }
                        }
                    }
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"[HATA] Base64 şifre çözme başarısız: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[HATA] Şifre çözme hatası: {ex.Message}");
                throw;
            }
        }

        // **Base64 doğrulama fonksiyonu**
        private static bool IsBase64String(string base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out _);
        }




    }
}
