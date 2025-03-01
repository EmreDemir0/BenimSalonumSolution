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
            builder.HasKey(e => e.Id); // Id alaný primary key olarak belirleniyor.

            // **Zorunlu alanlar ve uzunluk sýnýrlarý**
            builder.Property(e => e.Turu)
                   .IsRequired() // Turu zorunlu
                   .HasMaxLength(50); // Turu'nun maksimum uzunluðu 50 karakter olacak

            builder.Property(e => e.Tanimi)
                   .IsRequired() // Tanimi zorunlu
                   .HasMaxLength(100); // Tanimi'nin maksimum uzunluðu 100 karakter olacak

            // **Ýsteðe baðlý alanlar (nullable)**
            builder.Property(e => e.Aciklama)
                   .HasMaxLength(500); // Aciklama, isteðe baðlý, maksimum uzunluk 500 karakter
        }
    }
}
