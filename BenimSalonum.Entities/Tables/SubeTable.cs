using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class SubeTable
    {
        [Key]
        public int Id { get; set; }
        
        [Required, MaxLength(100)]
        public required string SubeAdi { get; set; }
        
        [Required, MaxLength(10)]
        public required string SubeKodu { get; set; } // Şube kodu (SB001, SB002 gibi)
        
        [MaxLength(200)]
        public string? Adres { get; set; }
        
        [MaxLength(20)]
        public string? Telefon { get; set; }
        
        [MaxLength(100)]
        public string? Email { get; set; }
        
        [MaxLength(100)]
        public string? WebSite { get; set; }
        
        [MaxLength(20)]
        public string? VergiDairesi { get; set; }
        
        [MaxLength(20)]
        public string? VergiNo { get; set; }
        
        public bool AktifMi { get; set; } = true;
        
        [MaxLength(100)]
        public string? LisansKodu { get; set; } // Her şubeye özel lisans kodu
        
        [Column(TypeName = "datetime2")]
        public DateTime? LisansBitisTarihi { get; set; } // Lisans bitiş tarihi
        
        public int KullaniciLimiti { get; set; } = 5; // Şubede tanımlanabilecek maksimum kullanıcı sayısı
        
        public bool EFaturaKullanimi { get; set; } = false; // Şubede e-fatura kullanımı açık mı?
        
        public bool WhatsappKullanimi { get; set; } = false; // Şubede WhatsApp kullanımı açık mı?
        
        public bool SmsKullanimi { get; set; } = false; // Şubede SMS kullanımı açık mı?
        
        public bool EMailKullanimi { get; set; } = false; // Şubede e-mail kullanımı açık mı?
        
        public bool EticaretEntegrasyonu { get; set; } = false; // Şubede e-ticaret entegrasyonu açık mı?
        
        [MaxLength(500)]
        public string? LogoUrl { get; set; } // Şube logosu
        
        [Column(TypeName = "datetime2")]
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
        
        public int OlusturanKullaniciId { get; set; }
        
        [Column(TypeName = "datetime2")]
        public DateTime? GuncellenmeTarihi { get; set; }
        
        public int? GuncelleyenKullaniciId { get; set; }

        // Navigation Properties
        public virtual ICollection<FaturaTable>? Faturalar { get; set; }
        public virtual ICollection<SiparisTable>? Siparisler { get; set; }
    }
}
