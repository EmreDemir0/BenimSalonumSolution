using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class KasaTable
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(30)]
        public required string KasaKodu { get; set; }

        [Required, MaxLength(100)]
        public required string KasaAdi { get; set; }

        [MaxLength(50)]
        public string? YetkiliKodu { get; set; }

        [MaxLength(100)]
        public string? YetkiliAdi { get; set; }

        [MaxLength(500)]
        public string? Aciklama { get; set; }
    }
}
