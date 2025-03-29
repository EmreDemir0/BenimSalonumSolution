using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class FaturaTable
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int SubeId { get; set; } // Faturanın kesildiği şube
        
        [Required]
        public int FaturaTuru { get; set; } // 1: Satış, 2: Alış, 3: İade, 4: Masraf
        
        [Required]
        public int FaturaTipi { get; set; } // 1: Normal, 2: E-Fatura, 3: E-Arşiv, 4: E-İrsaliye

        [Required, MaxLength(16)]
        public required string FaturaNo { get; set; } // Fatura numarası

        [Required, MaxLength(50)]
        public required string BelgeNo { get; set; } // Fiş/Fatura/İrsaliye No
        
        [MaxLength(50)]
        public string? EfaturaNo { get; set; } // E-Fatura/E-Arşiv numarası
        
        [MaxLength(50)]
        public string? EarsivNo { get; set; } // E-Arşiv numarası
        
        [MaxLength(50)]
        public string? EirsaliyeNo { get; set; } // E-İrsaliye numarası
        
        [Required]
        public int CariId { get; set; } // Müşteri/Tedarikçi ID
        
        [Required, Column(TypeName = "datetime2")]
        public DateTime FaturaTarihi { get; set; } // Fatura tarihi
        
        [Column(TypeName = "datetime2")]
        public DateTime? SevkTarihi { get; set; } // Sevk tarihi
        
        [Required, Column(TypeName = "datetime2")]
        public DateTime VadeTarihi { get; set; } // Vade tarihi

        [MaxLength(50)]
        public string? OdemeSekli { get; set; } // Ödeme şekli

        [MaxLength(500)]
        public string? Aciklama { get; set; } // Genel açıklama
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal AraToplam { get; set; } = 0; // Ara toplam (KDV hariç)
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal KdvToplam { get; set; } = 0; // KDV toplamı
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal IndirimTutar { get; set; } = 0; // İndirim tutarı
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal GenelToplam { get; set; } = 0; // Genel toplam
        
        public bool KdvDahil { get; set; } = false; // KDV dahil mi?
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal OdenenTutar { get; set; } = 0; // Ödenen tutar
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal KalanTutar { get; set; } = 0; // Kalan tutar
        
        public int FaturaDurumu { get; set; } = 1; // 1: Taslak, 2: Onaylandı, 3: E-Fatura Gönderildi, 4: İptal Edildi
        
        public int OdemeDurumu { get; set; } = 1; // 1: Ödenmedi, 2: Kısmi Ödendi, 3: Tamamen Ödendi
        
        // Orkestra E-Fatura Bilgileri
        [MaxLength(200)]
        public string? EfaturaGuid { get; set; } // E-Fatura GUID
        
        [MaxLength(100)]
        public string? EfaturaDurum { get; set; } // E-Fatura durumu
        
        [MaxLength(500)]
        public string? EfaturaHata { get; set; } // E-Fatura hata mesajı
        
        [Column(TypeName = "datetime2")]
        public DateTime? EfaturaGonderimTarihi { get; set; } // E-Fatura gönderim tarihi
        
        public bool EfaturaBasarili { get; set; } = false; // E-Fatura başarıyla gönderildi mi?
        
        [MaxLength(500)]
        public string? EfaturaPdf { get; set; } // E-Fatura PDF linki
        
        [MaxLength(500)]
        public string? EfaturaXml { get; set; } // E-Fatura XML linki
        
        // Müşteri/Tedarikçi Bilgileri (Fatura anında)
        [MaxLength(100)]
        public string? CariUnvan { get; set; } // Cari ünvanı
        
        [MaxLength(11)]
        public string? CariTckn { get; set; } // Cari TCKN
        
        [MaxLength(11)]
        public string? CariVkn { get; set; } // Cari VKN
        
        [MaxLength(30)]
        public string? CariVergiDairesi { get; set; } // Cari vergi dairesi
        
        [MaxLength(200)]
        public string? CariAdres { get; set; } // Cari adresi
        
        [MaxLength(30)]
        public string? CariIl { get; set; } // Cari ili
        
        [MaxLength(30)]
        public string? CariIlce { get; set; } // Cari ilçesi
        
        // İlişkilendirme
        public int? SiparisId { get; set; } // Bağlı olduğu sipariş
        
        public int? IrsaliyeId { get; set; } // Bağlı olduğu irsaliye
        
        // Mali dönem bilgileri
        public int MaliYil { get; set; } // Faturanın ait olduğu mali yıl
        
        public int MaliDonem { get; set; } // Faturanın ait olduğu mali dönem (1-12)
        
        // İzleme bilgileri
        [Column(TypeName = "datetime2")]
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
        
        public int OlusturanKullaniciId { get; set; }
        
        [Column(TypeName = "datetime2")]
        public DateTime? GuncellenmeTarihi { get; set; }
        
        public int? GuncelleyenKullaniciId { get; set; }
        
        [Column(TypeName = "datetime2")]
        public DateTime? OnayTarihi { get; set; }
        
        public int? OnaylayanKullaniciId { get; set; }
        
        [Column(TypeName = "datetime2")]
        public DateTime? IptalTarihi { get; set; }
        
        public int? IptalEdenKullaniciId { get; set; }
        
        [MaxLength(500)]
        public string? IptalNedeni { get; set; }

        // E-Fatura Entegrasyonu için yeni eklenen alanlar
        [MaxLength(50)]
        public string? GibFaturaDurumu { get; set; } // GİB'deki fatura durum bilgisi
        
        [MaxLength(100)]
        public string? PostaKutusuId { get; set; } // Alıcının posta kutusu ID'si
        
        [MaxLength(100)]
        public string? EtiketId { get; set; } // Alıcının etiket ID'si
        
        public int? EarsivRaporId { get; set; } // İlişkili e-arşiv rapor ID'si
        
        public int? KullanılanKontorAdet { get; set; } // Bu fatura için kullanılan kontör adedi

        // Navigation Properties
        public virtual CariTable? Cari { get; set; }
        public virtual SubeTable? Sube { get; set; }
        public virtual SiparisTable? Siparis { get; set; }
        public virtual ICollection<FaturaDetayTable>? FaturaDetaylari { get; set; }
        public virtual ICollection<EFaturaLogTable>? EFaturaLoglari { get; set; }
    }
}
