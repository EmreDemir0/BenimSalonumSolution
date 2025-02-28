using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class StokHareketTable
    {
        [Key] // Primary Key
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public required string FisKodu { get; set; } // Fiş numarası

        [Required, MaxLength(50)]
        public required string Hareket { get; set; } // Giriş, Çıkış gibi hareket türü

        [Required, ForeignKey("StokTable")]
        public int StokId { get; set; } // Stok ile ilişkilendirildi

        [Column(TypeName = "decimal(18,3)")]
        public decimal? Miktar { get; set; } // Opsiyonel miktar bilgisi

        [Required]
        public int Kdv { get; set; } // KDV oranı (%0, %8, %18 gibi)

        [Column(TypeName = "decimal(18,2)")]
        public decimal? BirimFiyati { get; set; } // Opsiyonel birim fiyat

        [Column(TypeName = "decimal(5,2)")]
        public decimal? IndirimOrani { get; set; } // Opsiyonel indirim yüzdesi

        [Required, ForeignKey("DepoTable")]
        public int DepoId { get; set; } // Depo bağlantısı

        [MaxLength(50)]
        public string? SeriNo { get; set; } // Opsiyonel seri numarası

        [Column(TypeName = "datetime2")]
        public DateTime? Tarih { get; set; } = DateTime.Now; // Varsayılan olarak şu anki tarih

        [MaxLength(500)]
        public string? Aciklama { get; set; } // Opsiyonel açıklama

        public bool Siparis { get; set; } = false; // Varsayılan olarak sipariş değil

        // Navigation Properties
        public StokTable? Stok { get; set; }
        public DepoTable? Depo { get; set; }
    }
}
