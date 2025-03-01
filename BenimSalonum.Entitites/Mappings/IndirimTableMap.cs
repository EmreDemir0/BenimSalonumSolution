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
            builder.HasKey(e => e.Id); // Id alaný primary key olarak belirleniyor.

            // **Varsayýlan deðerler**
            builder.Property(e => e.Durumu)
                   .HasDefaultValue(true); // Varsayýlan olarak indirim aktif

            // **Zorunlu alanlar ve uzunluk sýnýrlarý**
            builder.Property(e => e.StokKodu)
                   .IsRequired() // StokKodu zorunlu
                   .HasMaxLength(30); // StokKodu'nun maksimum uzunluðu 30 karakter olacak

            builder.Property(e => e.StokAdi)
                   .IsRequired() // StokAdi zorunlu
                   .HasMaxLength(100); // StokAdi alanýnýn maksimum uzunluðu 100 karakter olacak

            builder.Property(e => e.IndirimTuru)
                   .IsRequired() // IndirimTuru zorunlu
                   .HasMaxLength(50); // IndirimTuru'nun maksimum uzunluðu 50 karakter olacak

            builder.Property(e => e.BaslangicTarihi)
                   .IsRequired()
                   .HasColumnType("datetime2"); // Tarih formatý datetime2 olarak ayarlandý

            builder.Property(e => e.BitisTarihi)
                   .IsRequired()
                   .HasColumnType("datetime2"); // Tarih formatý datetime2 olarak ayarlandý

            builder.Property(e => e.IndirimOrani)
                   .IsRequired()
                   .HasColumnType("decimal(5,2)"); // IndirimOrani için 99.99 formatý kullanýldý

            // **Ýsteðe baðlý alanlar (nullable)**
            builder.Property(e => e.Barkod)
                   .HasMaxLength(50); // Barkod isteðe baðlý, maksimum uzunluk 50 karakter

            builder.Property(e => e.Aciklama)
                   .HasMaxLength(500); // Aciklama isteðe baðlý, maksimum uzunluk 500 karakter
        }
    }
}
