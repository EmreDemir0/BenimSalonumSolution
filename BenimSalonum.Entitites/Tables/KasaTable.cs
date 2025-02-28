using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class KasaTable
    {
        [Key] // Primary Key
        public int Id { get; set; }

        [Required, MaxLength(30)]
        public required string KasaKodu { get; set; }

        [Required, MaxLength(100)]
        public required string KasaAdi { get; set; }

        [MaxLength(50)]
        public string? YetkiliKodu { get; set; } // Opsiyonel alan (nullable)

        [MaxLength(100)]
        public string? YetkiliAdi { get; set; } // Opsiyonel alan (nullable)

        [MaxLength(500)]
        public string? Aciklama { get; set; } // Opsiyonel alan (nullable)

        // Navigation Property: Kasa ile ilgili hareketler
        public ICollection<KasaHareketTable>? KasaHareketleri { get; set; }
    }
}
