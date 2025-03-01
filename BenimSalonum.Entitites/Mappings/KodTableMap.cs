using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Mapping
{
    public class KodTableMap : IEntityTypeConfiguration<KodTable>
    {
        public void Configure(EntityTypeBuilder<KodTable> builder)
        {
            // **Primary Key**
            builder.HasKey(e => e.Id); // Id alaný primary key olarak belirleniyor.

            // **Zorunlu alanlar ve uzunluk sýnýrlarý**
            builder.Property(e => e.Tablo)
                   .IsRequired() // Tablo adý zorunlu
                   .HasMaxLength(50); // Tablo adýnýn maksimum uzunluðu 50 karakter olacak

            builder.Property(e => e.OnEki)
                   .IsRequired() // Ön ek zorunlu
                   .HasMaxLength(10); // Ön ek alanýnýn maksimum uzunluðu 10 karakter olacak

            builder.Property(e => e.SonDeger)
                   .IsRequired(); // SonDeger zorunlu, bu alan son kullanýlan deðeri tutacak
        }
    }
}
