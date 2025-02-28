using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class KasaHareketTable
    {
        [Key] // Primary Key
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public required string FisKodu { get; set; }

        [Required, MaxLength(50)]
        public required string Hareket { get; set; } // Gelir, Gider, Ödeme gibi

        [Required, ForeignKey("KasaTable")]
        public int KasaId { get; set; } // Bağlı olduğu Kasa

        [Required, ForeignKey("OdemeTuruTable")]
        public int OdemeTuruId { get; set; } // Ödeme türü bağlantısı

        public int? CariId { get; set; } // Opsiyonel, nullable bırakıldı

        [Required, Column(TypeName = "datetime2")]
        public DateTime Tarih { get; set; } // Varsayılan olarak şu anki tarih

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Tutar { get; set; } // Opsiyonel, nullable bırakıldı

        // Navigation Properties
        public KasaTable? Kasa { get; set; }
        public OdemeTuruTable? OdemeTuru { get; set; }
    }
}
