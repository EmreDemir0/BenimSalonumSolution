using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class FaturaDetayTable
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int FaturaId { get; set; } // Bağlı olduğu fatura ID
        
        public int SiraNo { get; set; } = 0; // Fatura üzerindeki sıra numarası
        
        public int? StokId { get; set; } // Stok ID (Hizmet ise null olabilir)
        
        [MaxLength(30)]
        public string? StokKodu { get; set; } // Stok kodu
        
        [Required, MaxLength(150)]
        public required string UrunAdi { get; set; } // Ürün/Hizmet adı
        
        [MaxLength(50)]
        public string? Barkod { get; set; } // Barkod
        
        [Required, MaxLength(20)]
        public required string Birim { get; set; } // Birim (Adet, Kg, vb.)
        
        [Column(TypeName = "decimal(18,3)")]
        public decimal Miktar { get; set; } = 0; // Miktar
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal BirimFiyat { get; set; } = 0; // Birim fiyat (KDV hariç)
        
        public int KdvOrani { get; set; } = 0; // KDV oranı (%)
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal KdvTutar { get; set; } = 0; // KDV tutarı
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal AraToplam { get; set; } = 0; // KDV hariç tutar (Miktar * BirimFiyat)
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal ToplamTutar { get; set; } = 0; // KDV dahil toplam tutar
        
        public int IndirimTuru { get; set; } = 0; // 0: Yok, 1: Yüzde, 2: Tutar
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal IndirimOrani { get; set; } = 0; // İndirim oranı (%)
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal IndirimTutar { get; set; } = 0; // İndirim tutarı
        
        // İsteğe bağlı alanlar
        [MaxLength(30)]
        public string? GTIPNo { get; set; } // Gümrük Tarife İstatistik Pozisyonu (İhracat faturalarında)
        
        public bool IskontoUygula { get; set; } = true; // Genel ıskontoyu bu kaleme uygula mı?
        
        [MaxLength(500)]
        public string? Aciklama { get; set; } // Kalem açıklaması
        
        public bool KdvDahil { get; set; } = false; // Fiyat KDV dahil mi girildi?
        
        // E-fatura için özel alanlar
        [MaxLength(50)]
        public string? EfaturaKod { get; set; } // E-fatura özel kodu
        
        [MaxLength(50)]
        public string? EfaturaTip { get; set; } // E-fatura özel tipi
        
        // Orkestra E-Fatura için TevkifatKodu ve Oranı
        [MaxLength(20)]
        public string? TevkifatKodu { get; set; } // Tevkifat kodu
        
        [Column(TypeName = "decimal(5,2)")]
        public decimal? TevkifatOrani { get; set; } // Tevkifat oranı
        
        // İzleme bilgileri
        [Column(TypeName = "datetime2")]
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
        
        public int OlusturanKullaniciId { get; set; }
        
        [Column(TypeName = "datetime2")]
        public DateTime? GuncellenmeTarihi { get; set; }
        
        public int? GuncelleyenKullaniciId { get; set; }
    }
}
