using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    /// <summary>
    /// Kullanıcı aktivitelerini loglamak için kullanılan tablo
    /// </summary>
    public class KullaniciLogTable
    {
        /// <summary>
        /// Benzersiz tanımlayıcı
        /// </summary>
        [Key] // Primary Key
        public int Id { get; set; }

        /// <summary>
        /// İşlemi yapan kullanıcının adı
        /// </summary>
        [Required, MaxLength(50)]
        public required string KullaniciAdi { get; set; }

        /// <summary>
        /// Kullanıcının son giriş tarihi
        /// </summary>
        [Column(TypeName = "datetime2")]
        public DateTime? SonGirisTarihi { get; set; } // Opsiyonel, nullable bırakıldı

        /// <summary>
        /// Kullanıcı tarafından yapılan işlem
        /// </summary>
        [Required, MaxLength(200)]
        public required string YapilanIslem { get; set; }

        /// <summary>
        /// İşlemin yapıldığı tarih
        /// </summary>
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime YapilanIslemTarihi { get; set; } = DateTime.Now;
        
        /// <summary>
        /// İşlem detayları (JSON formatında)
        /// </summary>
        [Column(TypeName = "text")]
        public string? Detay { get; set; }
        
        /// <summary>
        /// İşlemi yapan kullanıcının IP adresi
        /// </summary>
        [MaxLength(50)]
        public string? IpAdresi { get; set; }
        
        /// <summary>
        /// İlgili şube ID (varsa)
        /// </summary>
        public int? SubeId { get; set; }
        
        /// <summary>
        /// İlgili şube (navigation property)
        /// </summary>
        [ForeignKey("SubeId")]
        public virtual SubeTable? Sube { get; set; }
    }
}
