using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Mapping
{
    public class HizliSatisGrupTableMap : IEntityTypeConfiguration<HizliSatisGrupTable>
    {
        public void Configure(EntityTypeBuilder<HizliSatisGrupTable> builder)
        {
            // **Primary Key**
            builder.HasKey(e => e.Id); // Id alaný primary key olarak belirleniyor.

            // **Zorunlu alanlar ve uzunluk sýnýrlarý**
            builder.Property(e => e.GrupAdi)
                   .IsRequired() // GrupAdi alaný zorunlu
                   .HasMaxLength(100); // GrupAdi alanýnýn maksimum uzunluðu 100 karakter olacak

            // **Ekstra ayar (Varsa ekleyebilirsiniz)**
            // Eðer baþka konfigürasyonlar yapýlacaksa, örneðin indeks, default deðer vs., burada yapýlabilir.
        }
    }
}
