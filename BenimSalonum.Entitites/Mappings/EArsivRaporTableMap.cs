using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Mappings
{
    public class EArsivRaporTableMap : IEntityTypeConfiguration<EArsivRaporTable>
    {
        public void Configure(EntityTypeBuilder<EArsivRaporTable> builder)
        {
            // Tablo adı
            builder.ToTable("EArsivRaporlar");
            
            // Primary Key
            builder.HasKey(e => e.Id);
            
            // Alanlar
            builder.Property(e => e.RaporNo)
                   .HasMaxLength(50)
                   .IsRequired(false);
                   
            builder.Property(e => e.RaporTarihi)
                   .HasColumnType("datetime2")
                   .IsRequired();
                   
            builder.Property(e => e.BaslangicTarihi)
                   .HasColumnType("datetime2")
                   .IsRequired();
                   
            builder.Property(e => e.BitisTarihi)
                   .HasColumnType("datetime2")
                   .IsRequired();
                   
            builder.Property(e => e.Durum)
                   .IsRequired();
                   
            builder.Property(e => e.HataMesaji)
                   .HasMaxLength(500)
                   .IsRequired(false);
                   
            builder.Property(e => e.RaporUrl)
                   .HasMaxLength(500)
                   .IsRequired(false);
                   
            builder.Property(e => e.UUID)
                   .HasMaxLength(36)
                   .IsRequired(false);
                   
            builder.Property(e => e.SubeId)
                   .IsRequired();
                   
            builder.Property(e => e.KullaniciId)
                   .IsRequired();
                   
            builder.Property(e => e.Aciklama)
                   .HasMaxLength(500)
                   .IsRequired(false);
            
            // İzleme alanları
            builder.Property(e => e.OlusturmaTarihi)
                   .HasColumnType("datetime2")
                   .IsRequired();
                   
            builder.Property(e => e.OlusturanKullaniciId)
                   .IsRequired();
                   
            builder.Property(e => e.GuncellenmeTarihi)
                   .HasColumnType("datetime2")
                   .IsRequired(false);
                   
            builder.Property(e => e.GuncelleyenKullaniciId)
                   .IsRequired(false);
            
            // İlişkiler
            builder.HasOne(e => e.Sube)
                   .WithMany()
                   .HasForeignKey(e => e.SubeId)
                   .OnDelete(DeleteBehavior.Restrict);
                   
            // İndeksler
            builder.HasIndex(e => e.RaporNo);
            builder.HasIndex(e => e.SubeId);
            builder.HasIndex(e => e.UUID);
            builder.HasIndex(e => e.Durum);
            builder.HasIndex(e => e.RaporTarihi);
        }
    }
}
