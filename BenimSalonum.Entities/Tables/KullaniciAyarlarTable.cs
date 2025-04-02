using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class KullaniciAyarlarTable
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int KullaniciId { get; set; }
        
        // Bildirim ayarları
        public bool EmailBildirimAktif { get; set; } = true;         // E-posta bildirimleri
        public bool SMSBildirimAktif { get; set; } = true;           // SMS bildirimleri 
        public bool UygulamaIciBildirimAktif { get; set; } = true;   // Uygulama içi bildirimler
        
        // Randevu hatırlatma ayarları
        public int RandevuHatirlatmaZamani { get; set; } = 60;       // Dakika cinsinden (varsayılan 1 saat)
        
        // Arayüz ayarları
        [MaxLength(30)]
        public string Dil { get; set; } = "tr-TR";                   // Dil tercihi (varsayılan Türkçe)
        
        [MaxLength(30)]
        public string Tema { get; set; } = "Light";                  // Tema (Light/Dark)
        
        // Çalışma takvimi ayarları (personel için)
        [MaxLength(10)]
        public string CalismaBaslangicSaati { get; set; } = "09:00"; // Çalışma başlangıç saati
        
        [MaxLength(10)]
        public string CalismaBitisSaati { get; set; } = "18:00";     // Çalışma bitiş saati
        
        // Güvenlik ayarları
        public int OturumSuresi { get; set; } = 120;                 // Dakika cinsinden oturum süresi (varsayılan 2 saat)
        public bool OtomatikKilitlemeAktif { get; set; } = false;    // Belirli süre hareketsizlikten sonra kilitleme
        public int OtomatikKilitlemeSuresi { get; set; } = 15;       // Dakika cinsinden otomatik kilitleme süresi

        // Tarih ayarları
        [Column(TypeName = "datetime2")]
        public DateTime SonGuncellenmeTarihi { get; set; } = DateTime.Now;
        
        // İlişki
        [ForeignKey("KullaniciId")]
        public virtual KullaniciTable Kullanici { get; set; }
    }
}
