using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class AyarlarTable
    {
        [Key]
        public int Id { get; set; }
        
        public int SubeId { get; set; } = 0; // 0: Genel, > 0: Şubeye özel ayarlar
        
        [Required, MaxLength(100)]
        public required string AyarAdi { get; set; } // Ayar adı
        
        [Required, MaxLength(50)]
        public required string AyarGrubu { get; set; } // Ayar grubu (Yedekleme, SMS, Mail, Sistem, vb.)
        
        [Required, MaxLength(1000)]
        public required string AyarDegeri { get; set; } // Ayar değeri
        
        [MaxLength(500)]
        public string? Aciklama { get; set; } // Ayar açıklaması
        
        public int SiraNo { get; set; } = 0; // Görüntüleme sırası
        
        public bool AktifMi { get; set; } = true; // Ayar aktif mi?
        
        // Yedekleme ayarları için
        public int? YedeklemePeriyodu { get; set; } // 1: Günlük, 2: Haftalık, 3: Aylık
        
        public int? YedeklemeSaati { get; set; } // 0-23
        
        public int? YedeklemeSaklama { get; set; } // Kaç yedek saklanacak
        
        // SMS entegrasyonu için
        [MaxLength(100)]
        public string? SmsApiKey { get; set; }
        
        [MaxLength(100)]
        public string? SmsApiSecret { get; set; }
        
        [MaxLength(100)]
        public string? SmsApiUrl { get; set; }
        
        [MaxLength(30)]
        public string? SmsTitleId { get; set; } // SMS başlığı
        
        // Mail entegrasyonu için
        [MaxLength(100)]
        public string? MailHost { get; set; }
        
        public int? MailPort { get; set; }
        
        [MaxLength(100)]
        public string? MailKullanici { get; set; }
        
        [MaxLength(200)]
        public string? MailSifre { get; set; }
        
        public bool? MailSsl { get; set; }
        
        [MaxLength(100)]
        public string? MailGonderenAdi { get; set; }
        
        // E-fatura entegrasyonu için
        [MaxLength(200)]
        public string? EFaturaEntegratorUrl { get; set; }
        
        [MaxLength(100)]
        public string? EFaturaKullanici { get; set; }
        
        [MaxLength(200)]
        public string? EFaturaSifre { get; set; }
        
        // WhatsApp entegrasyonu için
        [MaxLength(200)]
        public string? WhatsappApiKey { get; set; }
        
        [MaxLength(100)]
        public string? WhatsappInstanceId { get; set; }
        
        [MaxLength(100)]
        public string? WhatsappPhoneNumber { get; set; }
        
        // Güncelleme izleme
        [Column(TypeName = "datetime2")]
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
        
        public int OlusturanKullaniciId { get; set; }
        
        [Column(TypeName = "datetime2")]
        public DateTime? GuncellenmeTarihi { get; set; }
        
        public int? GuncelleyenKullaniciId { get; set; }
    }
}
