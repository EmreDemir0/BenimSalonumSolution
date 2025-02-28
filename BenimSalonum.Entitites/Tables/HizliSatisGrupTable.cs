using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    public class HizliSatisGrupTable
    {
        [Key] // Primary Key
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public required string GrupAdi { get; set; }
    }
}
