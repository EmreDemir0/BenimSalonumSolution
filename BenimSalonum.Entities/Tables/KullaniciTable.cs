using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class KullaniciTable
    {
        [Key] // Primary Key
        public int Id { get; set; }

        public bool Aktif { get; set; } = true; // Varsayılan olarak aktif kullanıcı

        public bool Durumu { get; set; } = true; // Varsayılan olarak aktif kullanıcı

        [Required, MaxLength(50)]
        public required string KullaniciAdi { get; set; }

        [Required, MaxLength(50)]
        public required string Adi { get; set; }

        [Required, MaxLength(50)]
        public required string Soyadi { get; set; }

        [MaxLength(50)]
        public string? Gorevi { get; set; } = "User"; // Varsayılan olarak User rolü atanıyor

        [Required, MaxLength(100)]
        public required string Parola { get; set; }

        [MaxLength(100)]
        public string? HatirlatmaSorusu { get; set; } // Opsiyonel alan (nullable)

        [MaxLength(100)]
        public string? HatirlatmaCevap { get; set; } // Opsiyonel alan (nullable)

        [Column(TypeName = "datetime2")]
        public DateTime KayitTarihi { get; set; } = DateTime.Now; // Varsayılan olarak şu anki tarih

        [Column(TypeName = "datetime2")]
        public DateTime? SonGirisTarihi { get; set; } // Son giriş tarihi boş olabilir
        
        // Ana kullanıcı-alt kullanıcı ilişkisi için yeni alanlar
        public int? FirmaId { get; set; } // Bağlı olduğu firma
        
        public bool AnaKullanici { get; set; } = false; // Ana kullanıcı mı?
        
        public int? YoneticiId { get; set; } // Ana kullanıcı/yönetici ID (null ise kendisi ana kullanıcıdır)

        // Profil yönetimi için ek alanlar
        [MaxLength(100)]
        public string? Email { get; set; } // E-posta adresi

        [MaxLength(20)]
        public string? Telefon { get; set; } // Telefon numarası
        
        [MaxLength(500)]
        public string? ProfilResmiUrl { get; set; } // Profil resmi URL'i
        
        [MaxLength(1000)]
        public string? Adres { get; set; } // Kullanıcı adresi
        
        [MaxLength(30)]
        public string? Sehir { get; set; } // Şehir

        [Column(TypeName = "datetime2")]
        public DateTime? DogumTarihi { get; set; } // Doğum tarihi
        
        [MaxLength(20)]
        public string? Cinsiyet { get; set; } // Cinsiyet (Erkek, Kadın, Belirtmek İstemiyorum vb.)
        
        // Güvenlik ve hesap ayarları
        public bool IkiFaktorluKimlikDogrulama { get; set; } = false; // İki faktörlü kimlik doğrulama aktif mi?
        
        [MaxLength(100)]
        public string? IkiFaktorluDogrulamaAnahtari { get; set; } // 2FA için gizli anahtar
        
        [Column(TypeName = "datetime2")]
        public DateTime? SifreDegistirmeTarihi { get; set; } // En son şifre değiştirme tarihi
        
        public int BasarisizGirisDenemesi { get; set; } = 0; // Başarısız giriş denemesi sayısı
        
        [Column(TypeName = "datetime2")]
        public DateTime? HesapKilitlenmeTarihi { get; set; } // Hesap kilitlenme tarihi

        // Navigation Properties
        [ForeignKey("FirmaId")]
        public virtual FirmaTable? Firma { get; set; }
        
        [ForeignKey("YoneticiId")]
        public virtual KullaniciTable? Yonetici { get; set; }
    }
}
