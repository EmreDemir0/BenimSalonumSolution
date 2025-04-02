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

        // Stok miktarları (Platform bazlı stok takibi için)
        [Column(TypeName = "decimal(18,3)")]
        public decimal StokMiktari { get; set; } = 0; // Genel stok miktarı

        [Column(TypeName = "decimal(18,3)")]
        public decimal WebStokMiktari { get; set; } = 0; // Web sitesi için stok miktarı

        [Column(TypeName = "decimal(18,3)")]
        public decimal TrendyolStokMiktari { get; set; } = 0; // Trendyol için stok miktarı

        [Column(TypeName = "decimal(18,3)")]
        public decimal HepsiburadaStokMiktari { get; set; } = 0; // Hepsiburada için stok miktarı

        // Stok takip tipi (1: Bağımsız, 2: Genel Stoktan, 3: Web Stoktan)
        public int StokTakipTipi { get; set; } = 1;

        // Platform bazlı fiyat yapısı
        [Column(TypeName = "decimal(18,2)")]
        public decimal AlisFiyati { get; set; } = 0; // Ana alış fiyatı

        [Column(TypeName = "decimal(18,2)")]
        public decimal ToplamFiyati { get; set; } = 0; // Toptan satış fiyatı

        [Column(TypeName = "decimal(18,2)")]
        public decimal PerakendeFiyati { get; set; } = 0; // Perakende satış fiyatı

        [Column(TypeName = "decimal(18,2)")]
        public decimal WebFiyati { get; set; } = 0; // Web sitesi fiyatı

        [Column(TypeName = "decimal(18,2)")]
        public decimal TrendyolFiyati { get; set; } = 0; // Trendyol fiyatı

        [Column(TypeName = "decimal(18,2)")]
        public decimal HepsiburadaFiyati { get; set; } = 0; // Hepsiburada fiyatı

        // Fiyat hesaplama tipi (1: Bağımsız, 2: Toptan Fiyattan, 3: Perakende Fiyattan, 4: Özel Formül)
        public int FiyatHesaplamaTipi { get; set; } = 1;

        // E-ticaret entegrasyonu için gerekli alanlar
        [MaxLength(100)]
        public string? TrendyolKodu { get; set; } // Trendyol ürün kodu

        [MaxLength(100)]
        public string? HepsiburadaKodu { get; set; } // Hepsiburada ürün kodu

        [MaxLength(100)]
        public string? WebKodu { get; set; } // Web sitesi ürün kodu

        // Platformlar için komisyon oranları
        [Column(TypeName = "decimal(5,2)")]
        public decimal TrendyolKomisyon { get; set; } = 0; // Trendyol komisyon oranı (%)

        [Column(TypeName = "decimal(5,2)")]
        public decimal HepsiburadaKomisyon { get; set; } = 0; // Hepsiburada komisyon oranı (%)

        [Column(TypeName = "decimal(5,2)")]
        public decimal WebKomisyon { get; set; } = 0; // Web komisyon oranı (%)

        [Column(TypeName = "decimal(18,3)")]
        public decimal? MinStokMiktari { get; set; } // Opsiyonel

        [Column(TypeName = "decimal(18,3)")]
        public decimal? MaxStokMiktari { get; set; } // Opsiyonel

        [MaxLength(500)]
        public string? Aciklama { get; set; } // Opsiyonel açıklama

        [MaxLength(500)]
        public string? WebAciklama { get; set; } // Web sitesi için açıklama

        // Son güncelleme bilgileri
        [Column(TypeName = "datetime2")]
        public DateTime SonGuncelleme { get; set; } = DateTime.Now;

        public int GuncelleyenKullaniciId { get; set; } = 0;
    }
}
