using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class KullaniciTable
    {
        [Key] // Primary Key
        public int Id { get; set; }

        public bool Aktif { get; set; } = true; // Varsayılan olarak aktif kullanıcı

        public bool Durumu { get; set; } = true; // Varsayılan olarak aktif kullanıcı

        [Required, MaxLength(50)]
        public required string KullaniciAdi { get; set; }

        [Required, MaxLength(50)]
        public required string Adi { get; set; }

        [Required, MaxLength(50)]
        public required string Soyadi { get; set; }

        [MaxLength(50)]
        public string? Gorevi { get; set; } // Opsiyonel alan (nullable)

        [Required, MaxLength(100)]
        public required string Parola { get; set; }

        [MaxLength(100)]
        public string? HatirlatmaSorusu { get; set; } // Opsiyonel alan (nullable)

        [MaxLength(100)]
        public string? HatirlatmaCevap { get; set; } // Opsiyonel alan (nullable)

        [Column(TypeName = "datetime2")]
        public DateTime KayitTarihi { get; set; } = DateTime.Now; // Varsayılan olarak şu anki tarih

        [Column(TypeName = "datetime2")]
        public DateTime? SonGirisTarihi { get; set; } // Son giriş tarihi boş olabilir
    }
}
