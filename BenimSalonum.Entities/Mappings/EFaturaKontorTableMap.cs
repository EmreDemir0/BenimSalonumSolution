using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Mappings
{
    public class EFaturaKontorTableMap : IEntityTypeConfiguration<EFaturaKontorTable>
    {
        public void Configure(EntityTypeBuilder<EFaturaKontorTable> builder)
        {
            // Tablo adı
            builder.ToTable("EFaturaKontorlar");
            
            // Primary Key
            builder.HasKey(e => e.Id);
            
            // Alanlar
            builder.Property(e => e.SubeId)
                   .IsRequired();
                   
            builder.Property(e => e.ToplamKontor)
                   .IsRequired();
                   
            builder.Property(e => e.KalanKontor)
                   .IsRequired();
                   
            builder.Property(e => e.KullanilanKontor)
                   .IsRequired();
                   
            builder.Property(e => e.KontorTipi)
                   .IsRequired();
                   
            builder.Property(e => e.UstKontorId)
                   .IsRequired(false);
                   
            builder.Property(e => e.SatinAlmaTarihi)
                   .HasColumnType("datetime2")
                   .IsRequired();
                   
            builder.Property(e => e.SonKullanmaTarihi)
                   .HasColumnType("datetime2")
                   .IsRequired(false);
                   
            builder.Property(e => e.Aciklama)
                   .HasMaxLength(500)
                   .IsRequired(false);
                   
            builder.Property(e => e.Aktif)
                   .IsRequired();
                   
            builder.Property(e => e.SiparisNo)
                   .HasMaxLength(50)
                   .IsRequired(false);
                   
            builder.Property(e => e.FaturaNo)
                   .HasMaxLength(50)
                   .IsRequired(false);
                   
            builder.Property(e => e.Tutar)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();
                   
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
            // Şube ilişkisi
            builder.HasOne(e => e.Sube)
                   .WithMany()
                   .HasForeignKey(e => e.SubeId)
                   .OnDelete(DeleteBehavior.Restrict);
            
            // Üst kontör ilişkisi
            builder.HasOne(e => e.UstKontor)
                   .WithMany()
                   .HasForeignKey(e => e.UstKontorId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(false);
                   
            // İndeksler
            builder.HasIndex(e => e.SubeId);
            builder.HasIndex(e => e.UstKontorId);
            builder.HasIndex(e => e.SatinAlmaTarihi);
            builder.HasIndex(e => e.KontorTipi);
        }
    }
}
