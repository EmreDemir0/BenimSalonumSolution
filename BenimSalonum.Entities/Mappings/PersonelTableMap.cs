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
            builder.HasKey(e => e.Id); // Id alaný primary key olarak belirleniyor.

            // **Varsayýlan deðerler**
            builder.Property(e => e.Durumu)
                   .HasDefaultValue(true); // Varsayýlan olarak aktif personel

            builder.Property(e => e.PersonelGiris)
                   .HasDefaultValueSql("GETDATE()"); // PersonelGiris, varsayýlan olarak þu anki tarih

            // **Zorunlu alanlar ve uzunluk sýnýrlarý**
            builder.Property(e => e.PersonelUnvani)
                   .IsRequired() // PersonelUnvani zorunlu
                   .HasMaxLength(50); // PersonelUnvani'nin maksimum uzunluðu 50 karakter olacak

            builder.Property(e => e.PersonelKodu)
                   .IsRequired() // PersonelKodu zorunlu
                   .HasMaxLength(20); // PersonelKodu'nun maksimum uzunluðu 20 karakter olacak

            builder.Property(e => e.PersonelAdi)
                   .IsRequired() // PersonelAdi zorunlu
                   .HasMaxLength(100); // PersonelAdi'nin maksimum uzunluðu 100 karakter olacak

            builder.Property(e => e.PersonelTc)
                   .IsRequired() // PersonelTc zorunlu
                   .HasMaxLength(11); // PersonelTc'nin maksimum uzunluðu 11 karakter olacak

            builder.Property(e => e.Adres)
                   .IsRequired() // Adres zorunlu
                   .HasMaxLength(250); // Adres alanýnýn maksimum uzunluðu 250 karakter olacak

            // **Ýsteðe baðlý alanlar (nullable)**
            builder.Property(e => e.PersonelCikis)
                   .HasColumnType("datetime2"); // PersonelCikis, nullable, datetime2 formatýnda

            builder.Property(e => e.CepTelefonu)
                   .HasMaxLength(15) // CepTelefonu, maksimum 15 karakter olacak
                   .HasDefaultValue("0000000000"); // Varsayýlan deðer olarak "0000000000"

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
                   .HasColumnType("decimal(18,2)"); // AylikMaas için 18.2 formatý

            builder.Property(e => e.PrimOrani)
                   .HasColumnType("decimal(5,2)"); // PrimOrani için 5.2 formatý

            builder.Property(e => e.Aciklama)
                   .HasMaxLength(500); // Aciklama, maksimum 500 karakter olacak
        }
    }
}
