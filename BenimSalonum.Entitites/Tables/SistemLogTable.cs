using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    /// <summary>
    /// Sistem seviyesindeki loglar için tablo
    /// </summary>
    public class SistemLogTable
    {
        /// <summary>
        /// Benzersiz tanımlayıcı
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Log mesajı
        /// </summary>
        [Required, MaxLength(2000)]
        public required string Mesaj { get; set; }

        /// <summary>
        /// Hata seviyesi (0: Debug, 1: Information, 2: Warning, 3: Error, 4: Critical)
        /// </summary>
        [Required]
        public int HataSeviyesi { get; set; }

        /// <summary>
        /// Hangi modül üzerinde işlem yapıldı
        /// </summary>
        [MaxLength(100)]
        public string? Modul { get; set; }

        /// <summary>
        /// İşlem sırasında gönderilen HTTP isteğinin yolu
        /// </summary>
        [MaxLength(500)]
        public string? IstekYolu { get; set; }

        /// <summary>
        /// İşlemi yapan kullanıcı adı
        /// </summary>
        [MaxLength(100)]
        public string? KullaniciAdi { get; set; }

        /// <summary>
        /// İşlemi yapan kullanıcının IP adresi
        /// </summary>
        [MaxLength(50)]
        public string? IpAdresi { get; set; }

        /// <summary>
        /// Hata detayları (Exception içeriği)
        /// </summary>
        [Column(TypeName = "text")]
        public string? HataDetay { get; set; }

        /// <summary>
        /// Kayıt zamanı
        /// </summary>
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Tarih { get; set; } = DateTime.Now;

        /// <summary>
        /// İlgili şube ID (varsa)
        /// </summary>
        public int? SubeId { get; set; }

        /// <summary>
        /// Log ile ilgili ekstra veri (JSON formatında)
        /// </summary>
        public string? Veri { get; set; }

        /// <summary>
        /// Görünürlük seviyesi (0: Herkes, 1: Sadece Admin, 2: Sadece İlgili Kullanıcı)
        /// </summary>
        public int Gorünürlük { get; set; } = 0;

        /// <summary>
        /// İlgili şube (navigation property)
        /// </summary>
        [ForeignKey("SubeId")]
        public virtual SubeTable? Sube { get; set; }
    }
}
