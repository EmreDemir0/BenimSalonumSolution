using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Mapping
{
    public class FaturaDetayTableMap : IEntityTypeConfiguration<FaturaDetayTable>
    {
        public void Configure(EntityTypeBuilder<FaturaDetayTable> builder)
        {
            // Primary Key
            builder.HasKey(e => e.Id);
            
            // Foreign Keys
            builder.HasIndex(e => e.FaturaId)
                   .HasName("IX_FaturaDetay_FaturaId");
                   
            builder.HasIndex(e => e.StokId)
                   .HasName("IX_FaturaDetay_StokId");
                   
            // Zorunlu alanlar
            builder.Property(e => e.FaturaId)
                   .IsRequired();
                   
            builder.Property(e => e.UrunAdi)
                   .IsRequired()
                   .HasMaxLength(150);
                   
            builder.Property(e => e.Birim)
                   .IsRequired()
                   .HasMaxLength(20);
                   
            // İsteğe bağlı alanlar
            builder.Property(e => e.StokKodu)
                   .HasMaxLength(30);
                   
            builder.Property(e => e.Barkod)
                   .HasMaxLength(50);
                   
            builder.Property(e => e.GTIPNo)
                   .HasMaxLength(30);
                   
            builder.Property(e => e.Aciklama)
                   .HasMaxLength(500);
                   
            // E-fatura özel alanları
            builder.Property(e => e.EfaturaKod)
                   .HasMaxLength(50);
                   
            builder.Property(e => e.EfaturaTip)
                   .HasMaxLength(50);
                   
            builder.Property(e => e.TevkifatKodu)
                   .HasMaxLength(20);
                   
            builder.Property(e => e.TevkifatOrani)
                   .HasColumnType("decimal(5,2)");
                   
            // Sayısal değerler
            builder.Property(e => e.SiraNo)
                   .HasDefaultValue(0);
                   
            builder.Property(e => e.Miktar)
                   .HasColumnType("decimal(18,3)")
                   .HasDefaultValue(0);
                   
            builder.Property(e => e.BirimFiyat)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);
                   
            builder.Property(e => e.KdvOrani)
                   .HasDefaultValue(0);
                   
            builder.Property(e => e.KdvTutar)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);
                   
            builder.Property(e => e.AraToplam)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);
                   
            builder.Property(e => e.ToplamTutar)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);
                   
            builder.Property(e => e.IndirimTuru)
                   .HasDefaultValue(0);
                   
            builder.Property(e => e.IndirimOrani)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);
                   
            builder.Property(e => e.IndirimTutar)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);
                   
            // İzleme alanları
            builder.Property(e => e.OlusturmaTarihi)
                   .HasColumnType("datetime2")
                   .HasDefaultValueSql("GETDATE()");
                   
            builder.Property(e => e.GuncellenmeTarihi)
                   .HasColumnType("datetime2");
                   
            // İlişkiler
            builder.HasOne<FaturaTable>()
                   .WithMany()
                   .HasForeignKey(e => e.FaturaId)
                   .OnDelete(DeleteBehavior.Cascade);
                   
            builder.HasOne<StokTable>()
                   .WithMany()
                   .HasForeignKey(e => e.StokId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
