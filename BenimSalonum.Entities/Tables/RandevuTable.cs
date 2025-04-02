using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class RandevuTable
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int MusteriId { get; set; } // İlgili müşteri ID
        
        [Required]
        public int PersonelId { get; set; } // Randevuyu alan personel
        
        [Required]
        public int SubeId { get; set; } // Randevunun ait olduğu şube

        [Required]
        public int HizmetId { get; set; } // Alınacak hizmet türü
        
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime BaslangicTarihi { get; set; } // Randevu başlangıç zamanı
        
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime BitisTarihi { get; set; } // Randevu bitiş zamanı
        
        [MaxLength(500)]
        public string? Notlar { get; set; } // Randevu notları
        
        public int Durum { get; set; } = 1; // 1: Bekliyor, 2: Tamamlandı, 3: İptal Edildi, 4: Ertelendi
        
        public bool SmsGonderildi { get; set; } = false; // SMS hatırlatması gönderildi mi?
        
        public bool EmailGonderildi { get; set; } = false; // E-mail hatırlatması gönderildi mi?
        
        [Column(TypeName = "datetime2")]
        public DateTime? HatirlatmaTarihi { get; set; } // Hatırlatma yapılacak tarih
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Tutar { get; set; } = 0; // Randevu tutarı
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal IndirimTutari { get; set; } = 0; // İndirim tutarı
        
        public bool OdemeYapildi { get; set; } = false; // Ödeme yapıldı mı?
        
        public int OdemeTuruId { get; set; } = 0; // Ödeme türü
        
        [Column(TypeName = "datetime2")]
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now; // Randevu oluşturma tarihi
        
        public int OlusturanKullaniciId { get; set; } // Randevuyu oluşturan kullanıcı
        
        [Column(TypeName = "datetime2")]
        public DateTime? GuncellenmeTarihi { get; set; } // Randevu güncelleme tarihi
        
        public int? GuncelleyenKullaniciId { get; set; } // Randevuyu güncelleyen kullanıcı
        
        // İlişkiler burada tanımlanabilir (Navigation Properties)
        // [ForeignKey("MusteriId")]
        // public virtual CariTable Musteri { get; set; }
        
        // [ForeignKey("PersonelId")]
        // public virtual PersonelTable Personel { get; set; }
    }
}
