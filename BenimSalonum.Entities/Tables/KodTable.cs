using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class KodTable
    {
        [Key] // Primary Key
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public required string Tablo { get; set; } // Hangi tabloya ait kod olduğu

        [Required, MaxLength(10)]
        public required string OnEki { get; set; } // Kodu oluştururken kullanılacak ön ek

        [Required]
        public int SonDeger { get; set; } // Son kullanılan değer
    }
}
