using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class LisansTable
    {
        [Key]
        public int Id { get; set; }
        
        [Required, MaxLength(100)]
        public required string LisansKodu { get; set; }
        
        [Required]
        public int FirmaId { get; set; }
        
        [Required]
        public DateTime BaslangicTarihi { get; set; }
        
        [Required]
        public DateTime BitisTarihi { get; set; }
        
        [Required]
        public int KullaniciSayisiLimiti { get; set; } = 5;
        
        [Required]
        public bool Aktif { get; set; } = true;
        
        [MaxLength(250)]
        public string? AktivasyonAnahtari { get; set; }
        
        [Column(TypeName = "datetime2")]
        public DateTime? SonKontrolTarihi { get; set; }
        
        public int KalanKontrolGunu { get; set; } = 3;
        
        [MaxLength(50)]
        public string? LisansTuru { get; set; } = "Standart";
        
        [Column(TypeName = "datetime2")]
        public DateTime OlusturulmaTarihi { get; set; } = DateTime.Now;
        
        [MaxLength(250)]
        public string? Notlar { get; set; }
        
        // Navigation Properties
        [ForeignKey("FirmaId")]
        public virtual FirmaTable? Firma { get; set; }
    }
}
