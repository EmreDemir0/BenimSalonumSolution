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
            builder.HasKey(e => e.Id); // Id alan� primary key olarak belirleniyor.

            // **Zorunlu alanlar ve uzunluk s�n�rlar�**
            builder.Property(e => e.Tablo)
                   .IsRequired() // Tablo ad� zorunlu
                   .HasMaxLength(50); // Tablo ad�n�n maksimum uzunlu�u 50 karakter olacak

            builder.Property(e => e.OnEki)
                   .IsRequired() // �n ek zorunlu
                   .HasMaxLength(10); // �n ek alan�n�n maksimum uzunlu�u 10 karakter olacak

            builder.Property(e => e.SonDeger)
                   .IsRequired(); // SonDeger zorunlu, bu alan son kullan�lan de�eri tutacak
        }
    }
}
