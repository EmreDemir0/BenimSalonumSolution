using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class OdemeTuruTable
    {
        [Key] // Primary Key
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public required string OdemeTuruKodu { get; set; } // Ödeme türü kodu (örn: Nakit, Kredi Kartı vb.)

        [Required, MaxLength(50)]
        public required string OdemeTuruAdi { get; set; } // Ödeme türü adı

        [MaxLength(500)]
        public string? Aciklama { get; set; } // Opsiyonel açıklama

        // Navigation Property: Bu ödeme türüne bağlı kasa hareketleri olabilir
        public ICollection<KasaHareketTable>? KasaHareketleri { get; set; }
    }
}
