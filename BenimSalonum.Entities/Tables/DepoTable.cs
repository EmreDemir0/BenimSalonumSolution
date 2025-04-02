using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class DepoTable
    {
        [Key] // Primary Key
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public required string DepoKodu { get; set; }

        [Required, MaxLength(100)]
        public required string DepoAdi { get; set; }

        [MaxLength(50)]
        public string? YetkiliKodu { get; set; } // Opsiyonel alan (nullable)

        [MaxLength(100)]
        public string? YetkiliAdi { get; set; } // Opsiyonel alan (nullable)

        [MaxLength(50)]
        public string? Il { get; set; }

        [MaxLength(50)]
        public string? Ilce { get; set; }

        [MaxLength(50)]
        public string? Semt { get; set; } // Opsiyonel alan (nullable)

        [Required, MaxLength(250)]
        public required string Adres { get; set; }

        [MaxLength(15)]
        public string? Telefon { get; set; } // Opsiyonel alan (nullable)

        [MaxLength(500)]
        public string? Aciklama { get; set; } // Opsiyonel alan (nullable)
    }
}
