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
            builder.HasKey(e => e.Id); // Id alan� primary key olarak belirleniyor.

            // **Varsay�lan de�erler**
            builder.Property(e => e.Durumu)
                   .HasDefaultValue(true); // Varsay�lan olarak aktif stok

            // **Zorunlu alanlar ve uzunluk s�n�rlar�**
            builder.Property(e => e.StokKodu)
                   .IsRequired() // StokKodu zorunlu
                   .HasMaxLength(30); // StokKodu'nun maksimum uzunlu�u 30 karakter olacak

            builder.Property(e => e.StokAdi)
                   .IsRequired() // StokAdi zorunlu
                   .HasMaxLength(100); // StokAdi'nin maksimum uzunlu�u 100 karakter olacak

            builder.Property(e => e.Birimi)
                   .IsRequired() // Birimi zorunlu
                   .HasMaxLength(20); // Birimi'nin maksimum uzunlu�u 20 karakter olacak

            builder.Property(e => e.AlisKdv)
                   .IsRequired(); // AlisKdv zorunlu

            builder.Property(e => e.SatisKdv)
                   .IsRequired(); // SatisKdv zorunlu

            // **�ste�e ba�l� alanlar (nullable)**
            builder.Property(e => e.Barkod)
                   .HasMaxLength(50); // Barkod, iste�e ba�l�, maksimum uzunluk 50 karakter

            builder.Property(e => e.BarkodTuru)
                   .HasMaxLength(20); // BarkodTuru, iste�e ba�l�, maksimum uzunluk 20 karakter

            builder.Property(e => e.StokGrubu)
                   .HasMaxLength(50); // StokGrubu, iste�e ba�l�, maksimum uzunluk 50 karakter

            builder.Property(e => e.StokAltGrubu)
                   .HasMaxLength(50); // StokAltGrubu, iste�e ba�l�, maksimum uzunluk 50 karakter

            builder.Property(e => e.Marka)
                   .HasMaxLength(50); // Marka, iste�e ba�l�, maksimum uzunluk 50 karakter

            builder.Property(e => e.Modeli)
                   .HasMaxLength(50); // Modeli, iste�e ba�l�, maksimum uzunluk 50 karakter

            builder.Property(e => e.OzelKod1)
                   .HasMaxLength(30); // OzelKod1, iste�e ba�l�, maksimum uzunluk 30 karakter

            builder.Property(e => e.OzelKod2)
                   .HasMaxLength(30); // OzelKod2, iste�e ba�l�, maksimum uzunluk 30 karakter

            builder.Property(e => e.OzelKod3)
                   .HasMaxLength(30); // OzelKod3, iste�e ba�l�, maksimum uzunluk 30 karakter

            builder.Property(e => e.OzelKod4)
                   .HasMaxLength(30); // OzelKod4, iste�e ba�l�, maksimum uzunluk 30 karakter

            builder.Property(e => e.GarantiSuresi)
                   .HasMaxLength(20); // GarantiSuresi, iste�e ba�l�, maksimum uzunluk 20 karakter

            builder.Property(e => e.UreticiKodu)
                   .HasMaxLength(50); // UreticiKodu, iste�e ba�l�, maksimum uzunluk 50 karakter

            // **Decimal Alanlar**
            builder.Property(e => e.AlisFiyati1)
                   .HasColumnType("decimal(18,2)"); // AlisFiyati1, decimal(18,2) format�nda

            builder.Property(e => e.AlisFiyati2)
                   .HasColumnType("decimal(18,2)"); // AlisFiyati2, decimal(18,2) format�nda

            builder.Property(e => e.AlisFiyati3)
                   .HasColumnType("decimal(18,2)"); // AlisFiyati3, decimal(18,2) format�nda

            builder.Property(e => e.SatisFiyati1)
                   .HasColumnType("decimal(18,2)"); // SatisFiyati1, decimal(18,2) format�nda

            builder.Property(e => e.SatisFiyati2)
                   .HasColumnType("decimal(18,2)"); // SatisFiyati2, decimal(18,2) format�nda

            builder.Property(e => e.SatisFiyati3)
                   .HasColumnType("decimal(18,2)"); // SatisFiyati3, decimal(18,2) format�nda

            builder.Property(e => e.MinStokMiktari)
                   .HasColumnType("decimal(18,3)"); // MinStokMiktari, decimal(18,3) format�nda

            builder.Property(e => e.MaxStokMiktari)
                   .HasColumnType("decimal(18,3)"); // MaxStokMiktari, decimal(18,3) format�nda

            builder.Property(e => e.Aciklama)
                   .HasMaxLength(500); // Aciklama, iste�e ba�l�, maksimum uzunluk 500 karakter
        }
    }
}
