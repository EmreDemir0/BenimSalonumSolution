using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class KullaniciLogTable
    {
        [Key] // Primary Key
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public required string KullaniciAdi { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? SonGirisTarihi { get; set; } // Opsiyonel, nullable bırakıldı

        [Required, MaxLength(200)]
        public required string YapilanIslem { get; set; } // Kullanıcı tarafından yapılan işlem

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime YapilanIslemTarihi { get; set; } = DateTime.Now; // Varsayılan olarak şu anki tarih
    }
}
