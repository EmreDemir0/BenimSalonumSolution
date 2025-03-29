using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    /// <summary>
    /// E-Fatura kontör takibi için kullanılan tablo
    /// </summary>
    public class EFaturaKontorTable
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int SubeId { get; set; } // Şube ID - Kontör hangi şubeye ait
        
        [Required]
        public int ToplamKontor { get; set; } // Toplam satın alınan/atanan kontör miktarı
        
        [Required]
        public int KalanKontor { get; set; } // Kalan kontör miktarı
        
        [Required]
        public int KullanilanKontor { get; set; } // Kullanılan kontör miktarı
        
        [Required]
        public int KontorTipi { get; set; } // 1: Ana Hesap, 2: Alt Hesaba Dağıtılmış
        
        public int? UstKontorId { get; set; } // Üst kontör kaydı ID (alt hesaplar için)
        
        [Column(TypeName = "datetime2")]
        public DateTime SatinAlmaTarihi { get; set; } = DateTime.Now; // Kontörün satın alındığı tarih
        
        [Column(TypeName = "datetime2")]
        public DateTime? SonKullanmaTarihi { get; set; } // Kontörün son kullanma tarihi (varsa)
        
        [MaxLength(500)]
        public string? Aciklama { get; set; } // Açıklama
        
        [Required]
        public bool Aktif { get; set; } = true; // Aktif mi
        
        [MaxLength(50)]
        public string? SiparisNo { get; set; } // Kontör sipariş numarası
        
        [MaxLength(50)]
        public string? FaturaNo { get; set; } // Kontör fatura numarası
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Tutar { get; set; } = 0; // Kontör tutarı
        
        // İzleme bilgileri
        [Column(TypeName = "datetime2")]
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
        
        public int OlusturanKullaniciId { get; set; }
        
        [Column(TypeName = "datetime2")]
        public DateTime? GuncellenmeTarihi { get; set; }
        
        public int? GuncelleyenKullaniciId { get; set; }
        
        // Navigation Properties
        public virtual SubeTable? Sube { get; set; }
        
        public virtual EFaturaKontorTable? UstKontor { get; set; }
    }
}
