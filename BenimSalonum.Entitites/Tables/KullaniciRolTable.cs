using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class KullaniciRolTable
    {
        [Key] // Primary Key
        public int Id { get; set; }

        [Required]
        public int RootId { get; set; } // Ana root ID

        [Required]
        public int ParentId { get; set; } // Bağlı olduğu üst yetki

        [Required, MaxLength(50)]
        public required string KullaniciAdi { get; set; } // Kullanıcı Adı

        [Required, MaxLength(100)]
        public required string FormAdi { get; set; } // Form adı

        [Required, MaxLength(100)]
        public required string KontrolAdi { get; set; } // Kontrol adı (buton, menü vb.)

        public bool Yetki { get; set; } = false; // Varsayılan olarak yetkisiz
    }
}
