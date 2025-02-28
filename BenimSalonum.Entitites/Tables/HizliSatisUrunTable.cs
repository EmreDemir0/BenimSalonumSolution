using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class HizliSatisUrunTable
    {
        [Key] // Primary Key
        public int Id { get; set; }

        [MaxLength(50)]
        public string? Barkod { get; set; }

        [Required, MaxLength(100)]
        public required string UrunAdi { get; set; }

        [Required, ForeignKey("HizliSatisGrupTable")]
        public int GrupId { get; set; } // Foreign Key (Bağlı olduğu grup)

        public HizliSatisGrupTable? HizliSatisGrup { get; set; } // Navigation Property
    }
}
