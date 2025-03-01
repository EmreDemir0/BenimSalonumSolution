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
            builder.HasKey(e => e.Id); // Id alan� primary key olarak belirleniyor.

            // **Varsay�lan de�erler**
            builder.Property(e => e.Aktif)
                   .HasDefaultValue(true); // Varsay�lan olarak aktif kullan�c�

            builder.Property(e => e.Durumu)
                   .HasDefaultValue(true); // Varsay�lan olarak aktif kullan�c�

            // **Zorunlu alanlar ve uzunluk s�n�rlar�**
            builder.Property(e => e.KullaniciAdi)
                   .IsRequired() // KullaniciAdi zorunlu
                   .HasMaxLength(50); // KullaniciAdi'n�n maksimum uzunlu�u 50 karakter olacak

            builder.Property(e => e.Adi)
                   .IsRequired() // Adi zorunlu
                   .HasMaxLength(50); // Adi'nin maksimum uzunlu�u 50 karakter olacak

            builder.Property(e => e.Soyadi)
                   .IsRequired() // Soyadi zorunlu
                   .HasMaxLength(50); // Soyadi'nin maksimum uzunlu�u 50 karakter olacak

            builder.Property(e => e.Parola)
                   .IsRequired() // Parola zorunlu
                   .HasMaxLength(100); // Parola'n�n maksimum uzunlu�u 100 karakter olacak

            // **�ste�e ba�l� alanlar (nullable)**
            builder.Property(e => e.Gorevi)
                   .HasMaxLength(50); // Gorevi iste�e ba�l�, maksimum uzunluk 50 karakter

            builder.Property(e => e.HatirlatmaSorusu)
                   .HasMaxLength(100); // HatirlatmaSorusu iste�e ba�l�, maksimum uzunluk 100 karakter

            builder.Property(e => e.HatirlatmaCevap)
                   .HasMaxLength(100); // HatirlatmaCevap iste�e ba�l�, maksimum uzunluk 100 karakter

            // **Tarih alanlar�**
            builder.Property(e => e.KayitTarihi)
                   .IsRequired()
                   .HasColumnType("datetime2")
                   .HasDefaultValueSql("GETDATE()"); // KayitTarihi, �u anki tarih olarak varsay�lan

            builder.Property(e => e.SonGirisTarihi)
                   .HasColumnType("datetime2"); // SonGirisTarihi iste�e ba�l�, datetime2 format�nda
        }
    }
}
