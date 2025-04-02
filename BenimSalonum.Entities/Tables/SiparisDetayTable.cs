using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class SiparisDetayTable
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int SiparisId { get; set; } // Bağlı olduğu sipariş ID
        
        public int SiraNo { get; set; } = 0; // Sipariş üzerindeki sıra numarası
        
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
        
        [MaxLength(500)]
        public string? Aciklama { get; set; } // Kalem açıklaması
        
        public bool KdvDahil { get; set; } = false; // Fiyat KDV dahil mi girildi?
        
        public int SiparisDurumu { get; set; } = 1; // 1: Beklemede, 2: Onaylandı, 3: Kısmen Karşılandı, 4: Tamamen Karşılandı, 5: İptal
        
        [Column(TypeName = "decimal(18,3)")]
        public decimal KarsilananMiktar { get; set; } = 0; // Karşılanan miktar (faturalanan)
        
        // E-ticaret bilgileri
        [MaxLength(100)]
        public string? EticaretUrunKodu { get; set; } // E-ticaret platformu ürün kodu
        
        [MaxLength(36)]
        public string? EticaretSepetItemId { get; set; } // E-ticaret platformu sepet öğesi ID
        
        // İzleme bilgileri
        [Column(TypeName = "datetime2")]
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
        
        public int OlusturanKullaniciId { get; set; }
        
        [Column(TypeName = "datetime2")]
        public DateTime? GuncellenmeTarihi { get; set; }
        
        public int? GuncelleyenKullaniciId { get; set; }
    }
}
