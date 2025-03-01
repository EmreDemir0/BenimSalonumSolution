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
            builder.HasKey(e => e.Id); // Id alaný primary key olarak belirleniyor.

            // **Zorunlu alanlar ve uzunluk sýnýrlarý**
            builder.Property(e => e.FisKodu)
                   .IsRequired() // FisKodu zorunlu
                   .HasMaxLength(20); // FisKodu'nun maksimum uzunluðu 20 karakter olacak

            builder.Property(e => e.Unvani)
                   .IsRequired() // Unvani zorunlu
                   .HasMaxLength(50); // Unvani'nin maksimum uzunluðu 50 karakter olacak

            builder.Property(e => e.PersonelKodu)
                   .IsRequired() // PersonelKodu zorunlu
                   .HasMaxLength(20); // PersonelKodu'nun maksimum uzunluðu 20 karakter olacak

            builder.Property(e => e.PersonelAdi)
                   .IsRequired() // PersonelAdi zorunlu
                   .HasMaxLength(100); // PersonelAdi'nin maksimum uzunluðu 100 karakter olacak

            builder.Property(e => e.Donemi)
                   .IsRequired() // Donemi zorunlu
                   .HasColumnType("datetime2"); // Donem için datetime2 formatý kullanýldý

            builder.Property(e => e.PrimOrani)
                   .IsRequired() // PrimOrani zorunlu
                   .HasColumnType("decimal(5,2)"); // PrimOrani için 5.2 formatý

            builder.Property(e => e.ToplamSatis)
                   .IsRequired() // ToplamSatis zorunlu
                   .HasColumnType("decimal(18,2)"); // ToplamSatis için 18.2 formatý

            builder.Property(e => e.AylikMaas)
                   .IsRequired() // AylikMaas zorunlu
                   .HasColumnType("decimal(18,2)"); // AylikMaas için 18.2 formatý

            // **Ýsteðe baðlý alanlar (nullable)**
            builder.Property(e => e.TcKimlikNo)
                   .HasMaxLength(11); // TC Kimlik No'nun maksimum uzunluðu 11 karakter olacak

            builder.Property(e => e.Aciklama)
                   .HasMaxLength(500); // Aciklama isteðe baðlý, maksimum uzunluk 500 karakter
        }
    }
}
