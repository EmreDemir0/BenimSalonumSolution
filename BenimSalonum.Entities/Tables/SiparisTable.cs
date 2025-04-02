using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class SiparisTable
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int SubeId { get; set; } // Siparişin alındığı şube
        
        [Required]
        public int SiparisTuru { get; set; } // 1: Satış, 2: Alış
        
        [Required, MaxLength(20)]
        public required string SiparisNo { get; set; } // Sipariş numarası
        
        [Required]
        public int CariId { get; set; } // Müşteri/Tedarikçi ID
        
        [Required, Column(TypeName = "datetime2")]
        public DateTime SiparisTarihi { get; set; } // Sipariş tarihi
        
        [Column(TypeName = "datetime2")]
        public DateTime? TeslimTarihi { get; set; } // Teslim tarihi
        
        public int SiparisDurumu { get; set; } = 1; // 1: Beklemede, 2: Onaylandı, 3: Kargo/Sevkiyatta, 4: Tamamlandı, 5: İptal
        
        public int OnayDurumu { get; set; } = 1; // 1: Onay Bekliyor, 2: Onaylandı, 3: Reddedildi
        
        public int EticaretPlatformu { get; set; } = 0; // 0: Manuel, 1: Web, 2: Trendyol, 3: Hepsiburada, vb.
        
        [MaxLength(100)]
        public string? EticaretSiparisNo { get; set; } // E-ticaret platformundan gelen sipariş numarası
        
        [MaxLength(500)]
        public string? Aciklama { get; set; } // Sipariş açıklaması
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal AraToplam { get; set; } = 0; // Ara toplam (KDV hariç)
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal KdvToplam { get; set; } = 0; // KDV toplamı
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal IndirimTutar { get; set; } = 0; // İndirim tutarı
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal KargoTutar { get; set; } = 0; // Kargo tutarı
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal GenelToplam { get; set; } = 0; // Genel toplam
        
        // Fatura bilgileri
        public bool FaturaKesildi { get; set; } = false; // Fatura kesildi mi?
        
        public int? FaturaId { get; set; } // Kesilen fatura ID
        
        // Kargo/Sevkiyat bilgileri
        [MaxLength(50)]
        public string? KargoSirketi { get; set; } // Kargo şirketi
        
        [MaxLength(50)]
        public string? KargoTakipNo { get; set; } // Kargo takip numarası
        
        [Column(TypeName = "datetime2")]
        public DateTime? KargoTarihi { get; set; } // Kargoya verilme tarihi
        
        // Müşteri/Tedarikçi Bilgileri
        [MaxLength(100)]
        public string? CariUnvan { get; set; } // Cari ünvanı
        
        [MaxLength(200)]
        public string? TeslimatAdresi { get; set; } // Teslimat adresi
        
        [MaxLength(30)]
        public string? TeslimatIl { get; set; } // Teslimat ili
        
        [MaxLength(30)]
        public string? TeslimatIlce { get; set; } // Teslimat ilçesi
        
        [MaxLength(20)]
        public string? TelefonNo { get; set; } // Telefon numarası
        
        // Mali dönem bilgileri
        public int MaliYil { get; set; } // Siparişin ait olduğu mali yıl
        
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

        // Navigation Properties
        public virtual CariTable? Cari { get; set; }
        public virtual SubeTable? Sube { get; set; }
        public virtual FaturaTable? Fatura { get; set; }
        public virtual ICollection<SiparisDetayTable>? SiparisDetaylari { get; set; }
    }
}
