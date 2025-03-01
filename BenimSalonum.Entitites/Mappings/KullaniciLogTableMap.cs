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
            builder.HasKey(e => e.Id); // Id alan� primary key olarak belirleniyor.

            // **Zorunlu alanlar ve uzunluk s�n�rlar�**
            builder.Property(e => e.KullaniciAdi)
                   .IsRequired() // KullaniciAdi zorunlu
                   .HasMaxLength(50); // KullaniciAdi'n�n maksimum uzunlu�u 50 karakter olacak

            builder.Property(e => e.YapilanIslem)
                   .IsRequired() // YapilanIslem zorunlu
                   .HasMaxLength(200); // YapilanIslem'in maksimum uzunlu�u 200 karakter olacak

            builder.Property(e => e.YapilanIslemTarihi)
                   .IsRequired() // YapilanIslemTarihi zorunlu
                   .HasColumnType("datetime2") // datetime2 format�nda
                   .HasDefaultValueSql("GETDATE()"); // Varsay�lan de�er olarak �u anki tarih

            // **�ste�e ba�l� alan (nullable)**
            builder.Property(e => e.SonGirisTarihi)
                   .HasColumnType("datetime2"); // SonGirisTarihi, nullable, datetime2 format�nda
        }
    }
}
