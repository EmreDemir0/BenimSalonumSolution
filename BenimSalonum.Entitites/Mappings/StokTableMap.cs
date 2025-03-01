using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Mapping
{
    public class StokTableMap : IEntityTypeConfiguration<StokTable>
    {
        public void Configure(EntityTypeBuilder<StokTable> builder)
        {
            // **Primary Key**
            builder.HasKey(e => e.Id); // Id alaný primary key olarak belirleniyor.

            // **Varsayýlan deðerler**
            builder.Property(e => e.Durumu)
                   .HasDefaultValue(true); // Varsayýlan olarak aktif stok

            // **Zorunlu alanlar ve uzunluk sýnýrlarý**
            builder.Property(e => e.StokKodu)
                   .IsRequired() // StokKodu zorunlu
                   .HasMaxLength(30); // StokKodu'nun maksimum uzunluðu 30 karakter olacak

            builder.Property(e => e.StokAdi)
                   .IsRequired() // StokAdi zorunlu
                   .HasMaxLength(100); // StokAdi'nin maksimum uzunluðu 100 karakter olacak

            builder.Property(e => e.Birimi)
                   .IsRequired() // Birimi zorunlu
                   .HasMaxLength(20); // Birimi'nin maksimum uzunluðu 20 karakter olacak

            builder.Property(e => e.AlisKdv)
                   .IsRequired(); // AlisKdv zorunlu

            builder.Property(e => e.SatisKdv)
                   .IsRequired(); // SatisKdv zorunlu

            // **Ýsteðe baðlý alanlar (nullable)**
            builder.Property(e => e.Barkod)
                   .HasMaxLength(50); // Barkod, isteðe baðlý, maksimum uzunluk 50 karakter

            builder.Property(e => e.BarkodTuru)
                   .HasMaxLength(20); // BarkodTuru, isteðe baðlý, maksimum uzunluk 20 karakter

            builder.Property(e => e.StokGrubu)
                   .HasMaxLength(50); // StokGrubu, isteðe baðlý, maksimum uzunluk 50 karakter

            builder.Property(e => e.StokAltGrubu)
                   .HasMaxLength(50); // StokAltGrubu, isteðe baðlý, maksimum uzunluk 50 karakter

            builder.Property(e => e.Marka)
                   .HasMaxLength(50); // Marka, isteðe baðlý, maksimum uzunluk 50 karakter

            builder.Property(e => e.Modeli)
                   .HasMaxLength(50); // Modeli, isteðe baðlý, maksimum uzunluk 50 karakter

            builder.Property(e => e.OzelKod1)
                   .HasMaxLength(30); // OzelKod1, isteðe baðlý, maksimum uzunluk 30 karakter

            builder.Property(e => e.OzelKod2)
                   .HasMaxLength(30); // OzelKod2, isteðe baðlý, maksimum uzunluk 30 karakter

            builder.Property(e => e.OzelKod3)
                   .HasMaxLength(30); // OzelKod3, isteðe baðlý, maksimum uzunluk 30 karakter

            builder.Property(e => e.OzelKod4)
                   .HasMaxLength(30); // OzelKod4, isteðe baðlý, maksimum uzunluk 30 karakter

            builder.Property(e => e.GarantiSuresi)
                   .HasMaxLength(20); // GarantiSuresi, isteðe baðlý, maksimum uzunluk 20 karakter

            builder.Property(e => e.UreticiKodu)
                   .HasMaxLength(50); // UreticiKodu, isteðe baðlý, maksimum uzunluk 50 karakter

            // **Decimal Alanlar**
            builder.Property(e => e.AlisFiyati1)
                   .HasColumnType("decimal(18,2)"); // AlisFiyati1, decimal(18,2) formatýnda

            builder.Property(e => e.AlisFiyati2)
                   .HasColumnType("decimal(18,2)"); // AlisFiyati2, decimal(18,2) formatýnda

            builder.Property(e => e.AlisFiyati3)
                   .HasColumnType("decimal(18,2)"); // AlisFiyati3, decimal(18,2) formatýnda

            builder.Property(e => e.SatisFiyati1)
                   .HasColumnType("decimal(18,2)"); // SatisFiyati1, decimal(18,2) formatýnda

            builder.Property(e => e.SatisFiyati2)
                   .HasColumnType("decimal(18,2)"); // SatisFiyati2, decimal(18,2) formatýnda

            builder.Property(e => e.SatisFiyati3)
                   .HasColumnType("decimal(18,2)"); // SatisFiyati3, decimal(18,2) formatýnda

            builder.Property(e => e.MinStokMiktari)
                   .HasColumnType("decimal(18,3)"); // MinStokMiktari, decimal(18,3) formatýnda

            builder.Property(e => e.MaxStokMiktari)
                   .HasColumnType("decimal(18,3)"); // MaxStokMiktari, decimal(18,3) formatýnda

            builder.Property(e => e.Aciklama)
                   .HasMaxLength(500); // Aciklama, isteðe baðlý, maksimum uzunluk 500 karakter
        }
    }
}
