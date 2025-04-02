using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class KasaHareketTable
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(20)]
    public required string FisKodu { get; set; }

    [Required, MaxLength(50)]
    public required string Hareket { get; set; }

    [Required]
    public int KasaId { get; set; } // Sadece Kasa ID'sini tutar, ilişki yok

    [Required]
    public int OdemeTuruId { get; set; } // Sadece Ödeme Türü ID'sini tutar, ilişki yok

    public int? CariId { get; set; }

    [Required, Column(TypeName = "datetime2")]
    public DateTime Tarih { get; set; }

    [Required, Column(TypeName = "decimal(18,2)")]
    public decimal Tutar { get; set; }
}