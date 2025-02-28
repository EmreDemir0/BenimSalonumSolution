using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class TanimlarTable
    {
        [Key] // Primary Key
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public required string Turu { get; set; } // Tanım türü (Örn: Stok, Cari, Kasa)

        [Required, MaxLength(100)]
        public required string Tanimi { get; set; } // Tanım içeriği (Örn: Nakit, Kredi Kartı)

        [MaxLength(500)]
        public string? Aciklama { get; set; } // Opsiyonel açıklama
    }
}
