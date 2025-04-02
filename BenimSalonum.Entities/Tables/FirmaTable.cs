using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class FirmaTable
    {
        [Key]
        public int Id { get; set; }
        
        [Required, MaxLength(100)]
        public required string FirmaAdi { get; set; }
        
        [MaxLength(11)]
        public string? VergiNo { get; set; }
        
        [MaxLength(50)]
        public string? VergiDairesi { get; set; }
        
        [MaxLength(20)]
        public string? MersisNo { get; set; }
        
        [MaxLength(250)]
        public string? Adres { get; set; }
        
        [MaxLength(50)]
        public string? Il { get; set; }
        
        [MaxLength(50)]
        public string? Ilce { get; set; }
        
        [MaxLength(20)]
        public string? Telefon { get; set; }
        
        [MaxLength(100)]
        public string? Email { get; set; }
        
        [MaxLength(100)]
        public string? WebSitesi { get; set; }
        
        [Column(TypeName = "datetime2")]
        public DateTime OlusturulmaTarihi { get; set; } = DateTime.Now;
        
        [Column(TypeName = "datetime2")]
        public DateTime? GuncellenmeTarihi { get; set; }
        
        public int? OlusturanKullaniciId { get; set; }
        
        public int? GuncelleyenKullaniciId { get; set; }
        
        // Navigation Properties
        public virtual ICollection<KullaniciTable>? Kullanicilar { get; set; }
    }
}
