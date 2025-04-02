namespace BenimSalonum.Entities.DTOs
{
    // Kullanıcı ayarlarını görüntülemek için DTO
    public class KullaniciAyarlarDTO
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        
        // Bildirim ayarları
        public bool EmailBildirimAktif { get; set; }
        public bool SMSBildirimAktif { get; set; }
        public bool UygulamaIciBildirimAktif { get; set; }
        
        // Randevu hatırlatma ayarları
        public int RandevuHatirlatmaZamani { get; set; }
        
        // Arayüz ayarları
        public string Dil { get; set; }
        public string Tema { get; set; }
        
        // Çalışma takvimi ayarları (personel için)
        public string CalismaBaslangicSaati { get; set; }
        public string CalismaBitisSaati { get; set; }
        
        // Güvenlik ayarları
        public int OturumSuresi { get; set; }
        public bool OtomatikKilitlemeAktif { get; set; }
        public int OtomatikKilitlemeSuresi { get; set; }
    }

    // Kullanıcı ayarlarını güncellemek için DTO
    public class KullaniciAyarlarGuncellemeDTO
    {
        // Bildirim ayarları
        public bool EmailBildirimAktif { get; set; }
        public bool SMSBildirimAktif { get; set; }
        public bool UygulamaIciBildirimAktif { get; set; }
        
        // Randevu hatırlatma ayarları
        public int RandevuHatirlatmaZamani { get; set; }
        
        // Arayüz ayarları
        public string Dil { get; set; }
        public string Tema { get; set; }
    }

    // Çalışma saatleri ayarlarını güncellemek için DTO
    public class CalismaSaatleriGuncellemeDTO
    {
        public string CalismaBaslangicSaati { get; set; }
        public string CalismaBitisSaati { get; set; }
    }

    // Güvenlik ayarlarını güncellemek için DTO
    public class GuvenlikAyarlariGuncellemeDTO
    {
        public int OturumSuresi { get; set; }
        public bool OtomatikKilitlemeAktif { get; set; }
        public int OtomatikKilitlemeSuresi { get; set; }
    }
}
