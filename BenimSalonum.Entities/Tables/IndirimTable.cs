using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class IndirimTable
    {
        [Key] // Primary Key
        public int Id { get; set; }

        public bool Durumu { get; set; } = true; // Varsayılan olarak aktif indirim

        [Required, MaxLength(30)]
        public required string StokKodu { get; set; }

        [MaxLength(50)]
        public string? Barkod { get; set; }

        [Required, MaxLength(100)]
        public required string StokAdi { get; set; }

        [Required, MaxLength(50)]
        public required string IndirimTuru { get; set; }

        [Required, Column(TypeName = "datetime2")]
        public DateTime BaslangicTarihi { get; set; } // Opsiyonel, nullable bırakıldı

        [Required ,Column(TypeName = "datetime2")]
        public DateTime BitisTarihi { get; set; } // Opsiyonel, nullable bırakıldı

        [Required]
        [Column(TypeName = "decimal(5,2)")] // 99.99 formatı için
        public decimal IndirimOrani { get; set; }

        [MaxLength(500)]
        public string? Aciklama { get; set; } // Opsiyonel alan (nullable)
    }
}
