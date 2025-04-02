using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Mapping
{
    public class IndirimTableMap : IEntityTypeConfiguration<IndirimTable>
    {
        public void Configure(EntityTypeBuilder<IndirimTable> builder)
        {
            // **Primary Key**
            builder.HasKey(e => e.Id); // Id alan� primary key olarak belirleniyor.

            // **Varsay�lan de�erler**
            builder.Property(e => e.Durumu)
                   .HasDefaultValue(true); // Varsay�lan olarak indirim aktif

            // **Zorunlu alanlar ve uzunluk s�n�rlar�**
            builder.Property(e => e.StokKodu)
                   .IsRequired() // StokKodu zorunlu
                   .HasMaxLength(30); // StokKodu'nun maksimum uzunlu�u 30 karakter olacak

            builder.Property(e => e.StokAdi)
                   .IsRequired() // StokAdi zorunlu
                   .HasMaxLength(100); // StokAdi alan�n�n maksimum uzunlu�u 100 karakter olacak

            builder.Property(e => e.IndirimTuru)
                   .IsRequired() // IndirimTuru zorunlu
                   .HasMaxLength(50); // IndirimTuru'nun maksimum uzunlu�u 50 karakter olacak

            builder.Property(e => e.BaslangicTarihi)
                   .IsRequired()
                   .HasColumnType("datetime2"); // Tarih format� datetime2 olarak ayarland�

            builder.Property(e => e.BitisTarihi)
                   .IsRequired()
                   .HasColumnType("datetime2"); // Tarih format� datetime2 olarak ayarland�

            builder.Property(e => e.IndirimOrani)
                   .IsRequired()
                   .HasColumnType("decimal(5,2)"); // IndirimOrani i�in 99.99 format� kullan�ld�

            // **�ste�e ba�l� alanlar (nullable)**
            builder.Property(e => e.Barkod)
                   .HasMaxLength(50); // Barkod iste�e ba�l�, maksimum uzunluk 50 karakter

            builder.Property(e => e.Aciklama)
                   .HasMaxLength(500); // Aciklama iste�e ba�l�, maksimum uzunluk 500 karakter
        }
    }
}
