using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Mapping
{
    public class PersonelTableMap : IEntityTypeConfiguration<PersonelTable>
    {
        public void Configure(EntityTypeBuilder<PersonelTable> builder)
        {
            // **Primary Key**
            builder.HasKey(e => e.Id); // Id alan� primary key olarak belirleniyor.

            // **Varsay�lan de�erler**
            builder.Property(e => e.Durumu)
                   .HasDefaultValue(true); // Varsay�lan olarak aktif personel

            builder.Property(e => e.PersonelGiris)
                   .HasDefaultValueSql("GETDATE()"); // PersonelGiris, varsay�lan olarak �u anki tarih

            // **Zorunlu alanlar ve uzunluk s�n�rlar�**
            builder.Property(e => e.PersonelUnvani)
                   .IsRequired() // PersonelUnvani zorunlu
                   .HasMaxLength(50); // PersonelUnvani'nin maksimum uzunlu�u 50 karakter olacak

            builder.Property(e => e.PersonelKodu)
                   .IsRequired() // PersonelKodu zorunlu
                   .HasMaxLength(20); // PersonelKodu'nun maksimum uzunlu�u 20 karakter olacak

            builder.Property(e => e.PersonelAdi)
                   .IsRequired() // PersonelAdi zorunlu
                   .HasMaxLength(100); // PersonelAdi'nin maksimum uzunlu�u 100 karakter olacak

            builder.Property(e => e.PersonelTc)
                   .IsRequired() // PersonelTc zorunlu
                   .HasMaxLength(11); // PersonelTc'nin maksimum uzunlu�u 11 karakter olacak

            builder.Property(e => e.Adres)
                   .IsRequired() // Adres zorunlu
                   .HasMaxLength(250); // Adres alan�n�n maksimum uzunlu�u 250 karakter olacak

            // **�ste�e ba�l� alanlar (nullable)**
            builder.Property(e => e.PersonelCikis)
                   .HasColumnType("datetime2"); // PersonelCikis, nullable, datetime2 format�nda

            builder.Property(e => e.CepTelefonu)
                   .HasMaxLength(15) // CepTelefonu, maksimum 15 karakter olacak
                   .HasDefaultValue("0000000000"); // Varsay�lan de�er olarak "0000000000"

            builder.Property(e => e.Telefon)
                   .HasMaxLength(15); // Telefon, maksimum 15 karakter olacak

            builder.Property(e => e.Fax)
                   .HasMaxLength(15); // Fax, maksimum 15 karakter olacak

            builder.Property(e => e.EMail)
                   .HasMaxLength(100); // EMail, maksimum 100 karakter olacak

            builder.Property(e => e.Web)
                   .HasMaxLength(150); // Web, maksimum 150 karakter olacak

            builder.Property(e => e.Il)
                   .HasMaxLength(50); // Il, maksimum 50 karakter olacak

            builder.Property(e => e.Ilce)
                   .HasMaxLength(50); // Ilce, maksimum 50 karakter olacak

            builder.Property(e => e.Semt)
                   .HasMaxLength(50); // Semt, maksimum 50 karakter olacak

            builder.Property(e => e.AylikMaas)
                   .HasColumnType("decimal(18,2)"); // AylikMaas i�in 18.2 format�

            builder.Property(e => e.PrimOrani)
                   .HasColumnType("decimal(5,2)"); // PrimOrani i�in 5.2 format�

            builder.Property(e => e.Aciklama)
                   .HasMaxLength(500); // Aciklama, maksimum 500 karakter olacak
        }
    }
}
