using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Mapping
{
    public class HizliSatisUrunTableMap : IEntityTypeConfiguration<HizliSatisUrunTable>
    {
        public void Configure(EntityTypeBuilder<HizliSatisUrunTable> builder)
        {
            // **Primary Key**
            builder.HasKey(e => e.Id); // Id alan� primary key olarak belirleniyor.

            // **Zorunlu alanlar ve uzunluk s�n�rlar�**
            builder.Property(e => e.UrunAdi)
                   .IsRequired() // UrunAdi alan� zorunlu
                   .HasMaxLength(100); // UrunAdi alan�n�n maksimum uzunlu�u 100 karakter olacak

            // **�ste�e ba�l� alanlar**
            builder.Property(e => e.Barkod)
                   .HasMaxLength(50); // Barkod, iste�e ba�l� ve maksimum uzunluk 50 karakter

            // **Foreign Key (HizliSatisGrupTable)**
            builder.Property(e => e.GrupId)
                   .IsRequired(); // GrupId, foreign key oldu�u i�in zorunludur

            // **Navigation Property**
            builder.HasOne(e => e.HizliSatisGrup) // HizliSatisGrup ile ili�ki kuruldu
                   .WithMany() // HizliSatisGrupTable'da �oklu ili�kiler olabilir
                   .HasForeignKey(e => e.GrupId) // GrupId alan� foreign key olarak belirtiliyor
                   .OnDelete(DeleteBehavior.Restrict); // Silme i�lemi k�s�tland� (opsiyonel, istedi�iniz �ekilde de�i�tirebilirsiniz)
        }
    }
}
