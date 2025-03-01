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
            builder.HasKey(e => e.Id); // Id alan� primary key olarak belirleniyor.

            // **Zorunlu alanlar ve uzunluk s�n�rlar�**
            builder.Property(e => e.GrupAdi)
                   .IsRequired() // GrupAdi alan� zorunlu
                   .HasMaxLength(100); // GrupAdi alan�n�n maksimum uzunlu�u 100 karakter olacak

            // **Ekstra ayar (Varsa ekleyebilirsiniz)**
            // E�er ba�ka konfig�rasyonlar yap�lacaksa, �rne�in indeks, default de�er vs., burada yap�labilir.
        }
    }
}
