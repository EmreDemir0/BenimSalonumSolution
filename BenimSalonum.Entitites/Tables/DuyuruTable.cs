using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class DuyuruTable
    {
        [Key]
        public int Id { get; set; }
        
        [Required, MaxLength(200)]
        public required string Baslik { get; set; } // Duyuru başlığı
        
        [Required, MaxLength(5000)]
        public required string Icerik { get; set; } // Duyuru içeriği (HTML formatında olabilir)
        
        [MaxLength(500)]
        public string? ResimUrl { get; set; } // Duyuruya eklenecek resim URL'i
        
        public int DuyuruTipi { get; set; } = 1; // 1: Genel, 2: Özel, 3: Acil, 4: Kampanya
        
        public bool AktifMi { get; set; } = true; // Duyuru aktif mi?
        
        public int? HedefKitlee { get; set; } // 0: Tüm Kullanıcılar, 1: Sadece Yöneticiler, 2: Sadece Müşteriler, 3: Özel Liste
        
        public int? HedefSubeId { get; set; } // Duyurunun hedef şubesi (null ise tüm şubeler)
        
        [Column(TypeName = "datetime2")]
        public DateTime YayinBaslangic { get; set; } = DateTime.Now; // Duyuru yayın başlangıç tarihi
        
        [Column(TypeName = "datetime2")]
        public DateTime? YayinBitis { get; set; } // Duyuru yayın bitiş tarihi (null ise süresiz)
        
        [Column(TypeName = "datetime2")]
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now; // Duyuru oluşturma tarihi
        
        public int OlusturanKullaniciId { get; set; } // Duyuruyu oluşturan kullanıcı
        
        [Column(TypeName = "datetime2")]
        public DateTime? GuncellenmeTarihi { get; set; } // Duyuru güncelleme tarihi
        
        public int? GuncelleyenKullaniciId { get; set; } // Duyuruyu güncelleyen kullanıcı
        
        public bool OkunduBilgisiToplansin { get; set; } = false; // Duyurunun okundu bilgisi toplansın mı?
        
        public int GoruntulenmeSayisi { get; set; } = 0; // Duyurunun toplam görüntülenme sayısı
    }
}
