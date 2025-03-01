using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Mapping
{
    public class KullaniciLogTableMap : IEntityTypeConfiguration<KullaniciLogTable>
    {
        public void Configure(EntityTypeBuilder<KullaniciLogTable> builder)
        {
            // **Primary Key**
            builder.HasKey(e => e.Id); // Id alaný primary key olarak belirleniyor.

            // **Zorunlu alanlar ve uzunluk sýnýrlarý**
            builder.Property(e => e.KullaniciAdi)
                   .IsRequired() // KullaniciAdi zorunlu
                   .HasMaxLength(50); // KullaniciAdi'nýn maksimum uzunluðu 50 karakter olacak

            builder.Property(e => e.YapilanIslem)
                   .IsRequired() // YapilanIslem zorunlu
                   .HasMaxLength(200); // YapilanIslem'in maksimum uzunluðu 200 karakter olacak

            builder.Property(e => e.YapilanIslemTarihi)
                   .IsRequired() // YapilanIslemTarihi zorunlu
                   .HasColumnType("datetime2") // datetime2 formatýnda
                   .HasDefaultValueSql("GETDATE()"); // Varsayýlan deðer olarak þu anki tarih

            // **Ýsteðe baðlý alan (nullable)**
            builder.Property(e => e.SonGirisTarihi)
                   .HasColumnType("datetime2"); // SonGirisTarihi, nullable, datetime2 formatýnda
        }
    }
}
