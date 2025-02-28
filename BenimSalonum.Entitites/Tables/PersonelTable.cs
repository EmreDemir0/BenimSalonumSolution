using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class PersonelTable
    {
        [Key] // Primary Key
        public int Id { get; set; }

        public bool Durumu { get; set; } = true; // Varsayılan olarak aktif

        [Required, MaxLength(50)]
        public required string PersonelUnvani { get; set; } // Unvan (örn: Müdür, Kasiyer)

        [Required, MaxLength(20)]
        public required string PersonelKodu { get; set; } // Personel kodu

        [Required, MaxLength(100)]
        public required string PersonelAdi { get; set; } // Personel adı

        [Required, MaxLength(11)]
        public required string PersonelTc { get; set; } // TC Kimlik numarası (11 karakter)

        [Column(TypeName = "datetime2")]
        public DateTime? PersonelGiris { get; set; } = DateTime.Now; // Varsayılan olarak şu anki tarih

        [Column(TypeName = "datetime2")]
        public DateTime? PersonelCikis { get; set; } // Opsiyonel çıkış tarihi

        [Required, MaxLength(15)]
        public string CepTelefonu { get; set; } = "0000000000";// Opsiyonel alan

        [MaxLength(15)]
        public string? Telefon { get; set; } // Opsiyonel alan

        [MaxLength(15)]
        public string? Fax { get; set; } // Opsiyonel alan

        [EmailAddress, MaxLength(100)]
        public string? EMail { get; set; } // Opsiyonel e-posta

        [Url, MaxLength(150)]
        public string? Web { get; set; } // Opsiyonel web adresi

        [MaxLength(50)]
        public string? Il { get; set; } // İl bilgisi

        [MaxLength(50)]
        public  string? Ilce { get; set; } // İlçe bilgisi

        [MaxLength(50)]
        public string? Semt { get; set; } // Opsiyonel semt bilgisi

        [Required, MaxLength(250)]
        public required string Adres { get; set; } // Adres bilgisi

        [Column(TypeName = "decimal(18,2)")]
        public decimal? AylikMaas { get; set; } // Opsiyonel aylık maaş

        [Column(TypeName = "decimal(5,2)")]
        public decimal? PrimOrani { get; set; } // Opsiyonel prim oranı (örn: 5.50 gibi)

        [MaxLength(500)]
        public string? Aciklama { get; set; } // Opsiyonel açıklama
    }
}
