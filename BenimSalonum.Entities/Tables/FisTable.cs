using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class FisTable
    {
        [Key] // Primary Key
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public required string FisKodu { get; set; }

        [Required, MaxLength(50)]
        public required string FisTuru { get; set; }

        public int? CariId { get; set; } // Nullable, çünkü bazı fişlerde cari olmayabilir

        [MaxLength(100)]
        public string? FaturaUnvani { get; set; } // Opsiyonel

        [MaxLength(15)]
        public string? CepTelefonu { get; set; } // Opsiyonel

        [MaxLength(50)]
        public  string? Il { get; set; }

        [MaxLength(50)]
        public string? Ilce { get; set; }

        [MaxLength(50)]
        public string? Semt { get; set; } // Opsiyonel

        [MaxLength(250)]
        public string? Adres { get; set; }

        [MaxLength(50)]
        public string? VergiDairesi { get; set; } // Opsiyonel

        [MaxLength(20)]
        public string? VergiNo { get; set; } // Opsiyonel

        [MaxLength(30)]
        public string? BelgeNo { get; set; } // Opsiyonel

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Tarih { get; set; }// Varsayılan değer verildi

        public int? PlasiyerId { get; set; } // Opsiyonel, nullable bırakıldı

        [Column(TypeName = "decimal(18,2)")]
        public decimal? IskontoOrani { get; set; } // Opsiyonel, nullable bırakıldı

        [Column(TypeName = "decimal(18,2)")]
        public decimal? IskontoTutar { get; set; } // Opsiyonel, nullable bırakıldı

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Alacak { get; set; } // Opsiyonel, nullable bırakıldı

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Borc { get; set; } // Opsiyonel, nullable bırakıldı

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ToplamTutar { get; set; } // Opsiyonel, nullable bırakıldı

        [MaxLength(500)]
        public string? Aciklama { get; set; } // Opsiyonel

        [MaxLength(30)]
        public string? FisBaglantiKodu { get; set; } // Opsiyonel
    }
}
