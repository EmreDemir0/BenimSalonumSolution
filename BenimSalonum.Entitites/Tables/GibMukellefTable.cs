using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenimSalonum.Entities.Tables
{
    /// <summary>
    /// GIB mükellef sorgulamalarının sonuçlarını önbelleğe almak için kullanılan tablo
    /// </summary>
    public class GibMukellefTable
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(11)]
        public string VKN_TCKN { get; set; } // Vergi/TC kimlik no
        
        [MaxLength(100)]
        public string? Unvan { get; set; } // Mükellef unvanı
        
        [Required]
        public bool EFaturaKullanicisi { get; set; } // E-Fatura kullanıcısı mı?
        
        public bool EIrsaliyeKullanicisi { get; set; } // E-İrsaliye kullanıcısı mı?
        
        [Column(TypeName = "datetime2")]
        public DateTime SorgulamaTarihi { get; set; } = DateTime.Now; // Sorgulama tarihi
        
        [Column(TypeName = "datetime2")]
        public DateTime? GecerlilikTarihi { get; set; } // Bu tarihe kadar önbelleklenebilir
        
        [MaxLength(100)]
        public string? PostaKutusu { get; set; } // Posta kutusu adresi
        
        [MaxLength(100)]
        public string? Etiket { get; set; } // Etiket bilgisi
        
        [MaxLength(500)]
        public string? XmlYanit { get; set; } // XML yanıt (kısaltılmış)
        
        // İzleme bilgileri
        [Column(TypeName = "datetime2")]
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
        
        public int OlusturanKullaniciId { get; set; }
        
        [Column(TypeName = "datetime2")]
        public DateTime? GuncellenmeTarihi { get; set; }
        
        public int? GuncelleyenKullaniciId { get; set; }
    }
}
