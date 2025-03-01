using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Mapping
{
    public class PersonelHareketTableMap : IEntityTypeConfiguration<PersonelHareketTable>
    {
        public void Configure(EntityTypeBuilder<PersonelHareketTable> builder)
        {
            // **Primary Key**
            builder.HasKey(e => e.Id); // Id alan� primary key olarak belirleniyor.

            // **Zorunlu alanlar ve uzunluk s�n�rlar�**
            builder.Property(e => e.FisKodu)
                   .IsRequired() // FisKodu zorunlu
                   .HasMaxLength(20); // FisKodu'nun maksimum uzunlu�u 20 karakter olacak

            builder.Property(e => e.Unvani)
                   .IsRequired() // Unvani zorunlu
                   .HasMaxLength(50); // Unvani'nin maksimum uzunlu�u 50 karakter olacak

            builder.Property(e => e.PersonelKodu)
                   .IsRequired() // PersonelKodu zorunlu
                   .HasMaxLength(20); // PersonelKodu'nun maksimum uzunlu�u 20 karakter olacak

            builder.Property(e => e.PersonelAdi)
                   .IsRequired() // PersonelAdi zorunlu
                   .HasMaxLength(100); // PersonelAdi'nin maksimum uzunlu�u 100 karakter olacak

            builder.Property(e => e.Donemi)
                   .IsRequired() // Donemi zorunlu
                   .HasColumnType("datetime2"); // Donem i�in datetime2 format� kullan�ld�

            builder.Property(e => e.PrimOrani)
                   .IsRequired() // PrimOrani zorunlu
                   .HasColumnType("decimal(5,2)"); // PrimOrani i�in 5.2 format�

            builder.Property(e => e.ToplamSatis)
                   .IsRequired() // ToplamSatis zorunlu
                   .HasColumnType("decimal(18,2)"); // ToplamSatis i�in 18.2 format�

            builder.Property(e => e.AylikMaas)
                   .IsRequired() // AylikMaas zorunlu
                   .HasColumnType("decimal(18,2)"); // AylikMaas i�in 18.2 format�

            // **�ste�e ba�l� alanlar (nullable)**
            builder.Property(e => e.TcKimlikNo)
                   .HasMaxLength(11); // TC Kimlik No'nun maksimum uzunlu�u 11 karakter olacak

            builder.Property(e => e.Aciklama)
                   .HasMaxLength(500); // Aciklama iste�e ba�l�, maksimum uzunluk 500 karakter
        }
    }
}
