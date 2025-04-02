using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Mapping
{
    public class TanimlarTableMap : IEntityTypeConfiguration<TanimlarTable>
    {
        public void Configure(EntityTypeBuilder<TanimlarTable> builder)
        {
            // **Primary Key**
            builder.HasKey(e => e.Id); // Id alan� primary key olarak belirleniyor.

            // **Zorunlu alanlar ve uzunluk s�n�rlar�**
            builder.Property(e => e.Turu)
                   .IsRequired() // Turu zorunlu
                   .HasMaxLength(50); // Turu'nun maksimum uzunlu�u 50 karakter olacak

            builder.Property(e => e.Tanimi)
                   .IsRequired() // Tanimi zorunlu
                   .HasMaxLength(100); // Tanimi'nin maksimum uzunlu�u 100 karakter olacak

            // **�ste�e ba�l� alanlar (nullable)**
            builder.Property(e => e.Aciklama)
                   .HasMaxLength(500); // Aciklama, iste�e ba�l�, maksimum uzunluk 500 karakter
        }
    }
}
