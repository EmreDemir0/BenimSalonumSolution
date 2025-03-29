using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class EFaturaLogTable
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int FaturaId { get; set; } // İlgili fatura ID
        
        [Required]
        public int IslemTuru { get; set; } // 1: e-Fatura, 2: e-Arşiv, 3: e-İrsaliye, 4: e-SMM, 5: e-MM
        
        [Required]
        public int IslemDurumu { get; set; } // 1: Gönderim Bekleniyor, 2: Gönderildi, 3: Hata, 4: Başarılı, 5: İptal Edildi
        
        [Column(TypeName = "datetime2")]
        public DateTime IslemTarihi { get; set; } = DateTime.Now; // İşlem tarihi
        
        [MaxLength(36)]
        public string? UUID { get; set; } // Orkestra tarafından dönen UUID
        
        [MaxLength(50)]
        public string? BelgeNo { get; set; } // Belge numarası
        
        [MaxLength(500)]
        public string? RequestData { get; set; } // API isteği (kısaltılmış)
        
        [MaxLength(500)]
        public string? ResponseData { get; set; } // API yanıtı (kısaltılmış)
        
        [MaxLength(500)]
        public string? HataMesaji { get; set; } // Hata mesajı
        
        [MaxLength(500)]
        public string? PdfUrl { get; set; } // PDF URL
        
        [MaxLength(500)]
        public string? XmlUrl { get; set; } // XML URL
        
        [MaxLength(500)]
        public string? ZrfUrl { get; set; } // ZRF URL (e-Fatura için)
        
        [MaxLength(100)]
        public string? EttnNo { get; set; } // ETTN No
        
        public bool MailGonderildi { get; set; } = false; // Mail gönderildi mi?
        
        public bool SmsGonderildi { get; set; } = false; // SMS gönderildi mi?
        
        [Column(TypeName = "datetime2")]
        public DateTime? MailGonderimTarihi { get; set; } // Mail gönderim tarihi
        
        [Column(TypeName = "datetime2")]
        public DateTime? SmsGonderimTarihi { get; set; } // SMS gönderim tarihi
        
        [MaxLength(100)]
        public string? MailAdresi { get; set; } // Gönderilen mail adresi
        
        [MaxLength(20)]
        public string? TelefonNo { get; set; } // Gönderilen telefon numarası
        
        // İzleme bilgileri
        public int KullaniciId { get; set; } // İşlemi yapan kullanıcı
        
        [MaxLength(500)]
        public string? Aciklama { get; set; } // Açıklama
        
        // WSDL için yeni eklenen alanlar
        [MaxLength(100)]
        public string? EnvelopeId { get; set; } // WSDL'de kullanılan zarf ID'si
        
        public int? EarsivRaporDurumu { get; set; } // E-arşiv rapor durumu (1: Hazırlanıyor, 2: Tamamlandı, 3: Hata)
        
        [MaxLength(100)]
        public string? GibMesajId { get; set; } // GİB yanıt mesaj ID'si
        
        public int? KontorMiktari { get; set; } // İşlem için harcanan kontör miktarı
    }
}
