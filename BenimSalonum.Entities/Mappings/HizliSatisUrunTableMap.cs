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
            builder.HasKey(e => e.Id); // Id alaný primary key olarak belirleniyor.

            // **Zorunlu alanlar ve uzunluk sýnýrlarý**
            builder.Property(e => e.UrunAdi)
                   .IsRequired() // UrunAdi alaný zorunlu
                   .HasMaxLength(100); // UrunAdi alanýnýn maksimum uzunluðu 100 karakter olacak

            // **Ýsteðe baðlý alanlar**
            builder.Property(e => e.Barkod)
                   .HasMaxLength(50); // Barkod, isteðe baðlý ve maksimum uzunluk 50 karakter

            // **Foreign Key (HizliSatisGrupTable)**
            builder.Property(e => e.GrupId)
                   .IsRequired(); // GrupId, foreign key olduðu için zorunludur

            // **Navigation Property**
            builder.HasOne(e => e.HizliSatisGrup) // HizliSatisGrup ile iliþki kuruldu
                   .WithMany() // HizliSatisGrupTable'da çoklu iliþkiler olabilir
                   .HasForeignKey(e => e.GrupId) // GrupId alaný foreign key olarak belirtiliyor
                   .OnDelete(DeleteBehavior.Restrict); // Silme iþlemi kýsýtlandý (opsiyonel, istediðiniz þekilde deðiþtirebilirsiniz)
        }
    }
}
