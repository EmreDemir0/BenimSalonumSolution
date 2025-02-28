using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class PersonelHareketTable
    {
        [Key] // Primary Key
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public required string FisKodu { get; set; } // İşlem fişi kodu

        [Required, MaxLength(50)]
        public required string Unvani { get; set; } // Personelin unvanı (örn: Satış Temsilcisi)

        [Required, MaxLength(20)]
        public required string PersonelKodu { get; set; } // Personel kodu

        [Required, MaxLength(100)]
        public required string PersonelAdi { get; set; } // Personel adı

        [MaxLength(11)]
        public string? TcKimlikNo { get; set; } // TC Kimlik numarası (11 karakter)

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Donemi { get; set; } // Dönem bilgisi (örn: hangi aya ait işlem)

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal PrimOrani { get; set; } // Prim yüzdesi (örn: 5.50 gibi)

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ToplamSatis { get; set; } // Toplam satış miktarı

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AylikMaas { get; set; } // Aylık maaş miktarı

        [MaxLength(500)]
        public string? Aciklama { get; set; } // Opsiyonel açıklama
    }
}
