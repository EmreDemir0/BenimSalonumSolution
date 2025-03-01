using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Mapping
{
    public class KullaniciTableMap : IEntityTypeConfiguration<KullaniciTable>
    {
        public void Configure(EntityTypeBuilder<KullaniciTable> builder)
        {
            // **Primary Key**
            builder.HasKey(e => e.Id); // Id alaný primary key olarak belirleniyor.

            // **Varsayýlan deðerler**
            builder.Property(e => e.Aktif)
                   .HasDefaultValue(true); // Varsayýlan olarak aktif kullanýcý

            builder.Property(e => e.Durumu)
                   .HasDefaultValue(true); // Varsayýlan olarak aktif kullanýcý

            // **Zorunlu alanlar ve uzunluk sýnýrlarý**
            builder.Property(e => e.KullaniciAdi)
                   .IsRequired() // KullaniciAdi zorunlu
                   .HasMaxLength(50); // KullaniciAdi'nýn maksimum uzunluðu 50 karakter olacak

            builder.Property(e => e.Adi)
                   .IsRequired() // Adi zorunlu
                   .HasMaxLength(50); // Adi'nin maksimum uzunluðu 50 karakter olacak

            builder.Property(e => e.Soyadi)
                   .IsRequired() // Soyadi zorunlu
                   .HasMaxLength(50); // Soyadi'nin maksimum uzunluðu 50 karakter olacak

            builder.Property(e => e.Parola)
                   .IsRequired() // Parola zorunlu
                   .HasMaxLength(100); // Parola'nýn maksimum uzunluðu 100 karakter olacak

            // **Ýsteðe baðlý alanlar (nullable)**
            builder.Property(e => e.Gorevi)
                   .HasMaxLength(50); // Gorevi isteðe baðlý, maksimum uzunluk 50 karakter

            builder.Property(e => e.HatirlatmaSorusu)
                   .HasMaxLength(100); // HatirlatmaSorusu isteðe baðlý, maksimum uzunluk 100 karakter

            builder.Property(e => e.HatirlatmaCevap)
                   .HasMaxLength(100); // HatirlatmaCevap isteðe baðlý, maksimum uzunluk 100 karakter

            // **Tarih alanlarý**
            builder.Property(e => e.KayitTarihi)
                   .IsRequired()
                   .HasColumnType("datetime2")
                   .HasDefaultValueSql("GETDATE()"); // KayitTarihi, þu anki tarih olarak varsayýlan

            builder.Property(e => e.SonGirisTarihi)
                   .HasColumnType("datetime2"); // SonGirisTarihi isteðe baðlý, datetime2 formatýnda
        }
    }
}
