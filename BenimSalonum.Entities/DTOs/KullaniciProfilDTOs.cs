using System;

namespace BenimSalonum.Entities.DTOs
{
    // Kullanıcı profil bilgilerini görüntülemek için DTO
    public class KullaniciProfilDTO
    {
        public int Id { get; set; }
        public string KullaniciAdi { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string Gorevi { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string ProfilResmiUrl { get; set; }
        public string Adres { get; set; }
        public string Sehir { get; set; }
        public DateTime? DogumTarihi { get; set; }
        public string Cinsiyet { get; set; }
        public bool IkiFaktorluKimlikDogrulama { get; set; }
        public DateTime? SonGirisTarihi { get; set; }
    }

    // Kullanıcı profil bilgilerini güncellemek için DTO
    public class KullaniciProfilGuncellemeDTO
    {
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Adres { get; set; }
        public string Sehir { get; set; }
        public DateTime? DogumTarihi { get; set; }
        public string Cinsiyet { get; set; }
    }

    // Kullanıcı profil resmini güncellemek için DTO
    public class KullaniciProfilResmiGuncellemeDTO
    {
        public string ProfilResmiUrl { get; set; }
    }

    // Şifre değiştirme için DTO
    public class SifreDegistirmeDTO
    {
        public string MevcutSifre { get; set; }
        public string YeniSifre { get; set; }
        public string YeniSifreTekrar { get; set; }
    }

    // İki faktörlü kimlik doğrulama ayarları için DTO
    public class IkiFaktorluDogrulamaDTO
    {
        public bool Aktif { get; set; }
    }
}
