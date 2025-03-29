using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Mappings
{
    public class SistemLogTableMap : IEntityTypeConfiguration<SistemLogTable>
    {
        public void Configure(EntityTypeBuilder<SistemLogTable> builder)
        {
            // Tablo adı
            builder.ToTable("SistemLoglar");
            
            // Primary Key
            builder.HasKey(e => e.Id);
            
            // Alanlar
            builder.Property(e => e.Mesaj)
                   .HasMaxLength(2000)
                   .IsRequired();
                   
            builder.Property(e => e.HataSeviyesi)
                   .IsRequired();
                   
            builder.Property(e => e.Modul)
                   .HasMaxLength(100)
                   .IsRequired(false);
                   
            builder.Property(e => e.IstekYolu)
                   .HasMaxLength(500)
                   .IsRequired(false);
                   
            builder.Property(e => e.KullaniciAdi)
                   .HasMaxLength(100)
                   .IsRequired(false);
                   
            builder.Property(e => e.IpAdresi)
                   .HasMaxLength(50)
                   .IsRequired(false);
                   
            builder.Property(e => e.HataDetay)
                   .HasColumnType("text")
                   .IsRequired(false);
                   
            builder.Property(e => e.Tarih)
                   .HasColumnType("datetime2")
                   .IsRequired();
                   
            builder.Property(e => e.SubeId)
                   .IsRequired(false);
                   
            builder.Property(e => e.Veri)
                   .HasColumnType("text")
                   .IsRequired(false);
                   
            builder.Property(e => e.Gorünürlük)
                   .IsRequired()
                   .HasDefaultValue(0);
            
            // İlişkiler
            builder.HasOne(e => e.Sube)
                   .WithMany()
                   .HasForeignKey(e => e.SubeId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(false);
                   
            // İndeksler
            builder.HasIndex(e => e.HataSeviyesi);
            builder.HasIndex(e => e.Tarih);
            builder.HasIndex(e => e.KullaniciAdi);
            builder.HasIndex(e => e.SubeId);
            builder.HasIndex(e => e.Gorünürlük);
        }
    }
}
