using System;

namespace BenimSalonum.Entities.Tables
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string UserId { get; set; }  // Kullanıcıya bağlı token
        public string Token { get; set; }  // Token değeri
        public DateTime Expires { get; set; }  // Son kullanma tarihi
        public bool IsRevoked { get; set; }  // İptal Edilmiş Mi?
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public string? DeviceName { get; set; }     // Örn: iPhone 13, Chrome/Windows
        public string? Platform { get; set; }       // Örn: Web, Android, Windows, iOS


    }
}
