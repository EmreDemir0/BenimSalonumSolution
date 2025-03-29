using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Mappings
{
    public class KullaniciLogTableMap : IEntityTypeConfiguration<KullaniciLogTable>
    {
        public void Configure(EntityTypeBuilder<KullaniciLogTable> builder)
        {
            // Tablo adı
            builder.ToTable("KullaniciLoglar");
            
            // Primary Key
            builder.HasKey(e => e.Id);

            // Zorunlu alanlar ve uzunluk sınırları
            builder.Property(e => e.KullaniciAdi)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(e => e.YapilanIslem)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(e => e.YapilanIslemTarihi)
                   .IsRequired()
                   .HasColumnType("datetime2")
                   .HasDefaultValueSql("GETDATE()");

            // İsteğe bağlı alanlar
            builder.Property(e => e.SonGirisTarihi)
                   .HasColumnType("datetime2");
                   
            // Yeni eklenen alanlar
            builder.Property(e => e.Detay)
                   .HasColumnType("text")
                   .IsRequired(false);
                   
            builder.Property(e => e.IpAdresi)
                   .HasMaxLength(50)
                   .IsRequired(false);
                   
            builder.Property(e => e.SubeId)
                   .IsRequired(false);
            
            // İlişkiler
            builder.HasOne(e => e.Sube)
                   .WithMany()
                   .HasForeignKey(e => e.SubeId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(false);
                   
            // İndeksler
            builder.HasIndex(e => e.KullaniciAdi);
            builder.HasIndex(e => e.YapilanIslemTarihi);
            builder.HasIndex(e => e.SubeId);
        }
    }
}
