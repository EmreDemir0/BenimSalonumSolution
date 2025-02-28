using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class StokTable
    {
        [Key] // Primary Key
        public int Id { get; set; } // Ürün ID

        public bool Durumu { get; set; } = true; // Varsayılan olarak aktif

        [Required, MaxLength(30)]
        public required string StokKodu { get; set; }

        [Required, MaxLength(100)]
        public required string StokAdi { get; set; }

        [MaxLength(50)]
        public string? Barkod { get; set; } // Opsiyonel

        [MaxLength(20)]
        public string? BarkodTuru { get; set; } // Opsiyonel

        [Required, MaxLength(20)]
        public required string Birimi { get; set; } // Örn: Adet, KG

        [MaxLength(50)]
        public string? StokGrubu { get; set; } // Opsiyonel

        [MaxLength(50)]
        public string? StokAltGrubu { get; set; } // Opsiyonel

        [MaxLength(50)]
        public string? Marka { get; set; } // Opsiyonel

        [MaxLength(50)]
        public string? Modeli { get; set; } // Opsiyonel

        [MaxLength(30)]
        public string? OzelKod1 { get; set; } // Opsiyonel

        [MaxLength(30)]
        public string? OzelKod2 { get; set; } // Opsiyonel

        [MaxLength(30)]
        public string? OzelKod3 { get; set; } // Opsiyonel

        [MaxLength(30)]
        public string? OzelKod4 { get; set; } // Opsiyonel

        [MaxLength(20)]
        public string? GarantiSuresi { get; set; } // Opsiyonel

        [MaxLength(50)]
        public string? UreticiKodu { get; set; } // Opsiyonel

        [Required]
        public int AlisKdv { get; set; } // KDV zorunlu

        [Required]
        public int SatisKdv { get; set; } // KDV zorunlu

        [Column(TypeName = "decimal(18,2)")]
        public decimal? AlisFiyati1 { get; set; } // Opsiyonel

        [Column(TypeName = "decimal(18,2)")]
        public decimal? AlisFiyati2 { get; set; } // Opsiyonel

        [Column(TypeName = "decimal(18,2)")]
        public decimal? AlisFiyati3 { get; set; } // Opsiyonel

        [Column(TypeName = "decimal(18,2)")]
        public decimal? SatisFiyati1 { get; set; } // Opsiyonel

        [Column(TypeName = "decimal(18,2)")]
        public decimal? SatisFiyati2 { get; set; } // Opsiyonel

        [Column(TypeName = "decimal(18,2)")]
        public decimal? SatisFiyati3 { get; set; } // Opsiyonel

        [Column(TypeName = "decimal(18,3)")]
        public decimal? MinStokMiktari { get; set; } // Opsiyonel

        [Column(TypeName = "decimal(18,3)")]
        public decimal? MaxStokMiktari { get; set; } // Opsiyonel

        [MaxLength(500)]
        public string? Aciklama { get; set; } // Opsiyonel açıklama
    }
}
