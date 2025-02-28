using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class CariTable
    {
        [Key]
        public int Id { get; set; }

        public bool Durumu { get; set; } = true;

        [Required, MaxLength(50)]
        public required string CariTuru { get; set; }

        [Required, MaxLength(20)]
        public required string CariKodu { get; set; }

        [Required, MaxLength(100)]
        public required string CariAdi { get; set; }

        [MaxLength(50)]
        public string? YetkiliKisi { get; set; }

        [Required, MaxLength(100)]
        public required string FaturaUnvani { get; set; }

        [MaxLength(15)]
        public string? CepTelefonu { get; set; }

        [MaxLength(15)]
        public string? Telefon { get; set; }

        [MaxLength(15)]
        public string? Fax { get; set; }

        [EmailAddress, MaxLength(100)]
        public string? EMail { get; set; }

        [Url, MaxLength(150)]
        public string? Web { get; set; }

        [Required, MaxLength(50)]
        public required string Il { get; set; }

        [MaxLength(50)]
        public  string? Ilce { get; set; }

        [MaxLength(50)]
        public string? Semt { get; set; }

        [Required, MaxLength(250)]
        public required string Adres { get; set; }

        [MaxLength(50)]
        public string? CariGrubu { get; set; }

        [MaxLength(50)]
        public string? CariAltGrubu { get; set; }

        [MaxLength(30)]
        public string? OzelKod1 { get; set; }

        [MaxLength(30)]
        public string? OzelKod2 { get; set; }

        [MaxLength(30)]
        public string? OzelKod3 { get; set; }

        [MaxLength(30)]
        public string? OzelKod4 { get; set; }

        [MaxLength(50)]
        public string? VergiDairesi { get; set; }

        [MaxLength(20)]
        public string? VergiNo { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? IskontoOrani { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? RiskLimiti { get; set; }

        [MaxLength(500)]
        public string? Aciklama { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime KayitTarihi { get; set; } = DateTime.Now;
    }
}
