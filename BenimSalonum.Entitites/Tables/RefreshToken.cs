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
    }
}
