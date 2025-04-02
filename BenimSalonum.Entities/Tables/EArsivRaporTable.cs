using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BenimSalonum.Entities.Tables
{
    /// <summary>
    /// E-Arşiv raporlarını saklamak için kullanılan tablo
    /// </summary>
    public class EArsivRaporTable
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(50)]
        public string? RaporNo { get; set; } // Rapor numarası
        
        [Column(TypeName = "datetime2")]
        public DateTime RaporTarihi { get; set; } = DateTime.Now; // Rapor tarihi
        
        [Column(TypeName = "datetime2")]
        public DateTime BaslangicTarihi { get; set; } // Rapor başlangıç tarihi
        
        [Column(TypeName = "datetime2")]
        public DateTime BitisTarihi { get; set; } // Rapor bitiş tarihi
        
        [Required]
        public int Durum { get; set; } // 1: Hazırlanıyor, 2: Tamamlandı, 3: Hata, 4: İptal
        
        [MaxLength(500)]
        public string? HataMesaji { get; set; } // Hata mesajı
        
        [MaxLength(500)]
        public string? RaporUrl { get; set; } // Rapor URL
        
        [MaxLength(36)]
        public string? UUID { get; set; } // Orkestra tarafından dönen UUID
        
        [Required]
        public int SubeId { get; set; } // Şube ID
        
        [Required]
        public int KullaniciId { get; set; } // İşlemi yapan kullanıcı
        
        [MaxLength(500)]
        public string? Aciklama { get; set; } // Açıklama
        
        [Column(TypeName = "datetime2")]
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
        
        public int OlusturanKullaniciId { get; set; }
        
        [Column(TypeName = "datetime2")]
        public DateTime? GuncellenmeTarihi { get; set; }
        
        public int? GuncelleyenKullaniciId { get; set; }
        
        // Navigation Properties
        public virtual SubeTable? Sube { get; set; }
        
        // Faturalar ilişkisi - bir raporda birden fazla fatura olabilir
        public virtual ICollection<FaturaTable>? Faturalar { get; set; }
    }
}
