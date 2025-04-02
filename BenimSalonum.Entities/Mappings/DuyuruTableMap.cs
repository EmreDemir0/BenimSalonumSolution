using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Mapping
{
    public class DuyuruTableMap : IEntityTypeConfiguration<DuyuruTable>
    {
        public void Configure(EntityTypeBuilder<DuyuruTable> builder)
        {
            // Primary Key
            builder.HasKey(e => e.Id);

            // Zorunlu alanlar
            builder.Property(e => e.Baslik)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(e => e.Icerik)
                   .IsRequired()
                   .HasMaxLength(5000);

            // İsteğe bağlı alanlar
            builder.Property(e => e.ResimUrl)
                   .HasMaxLength(500);

            // Varsayılan değerler
            builder.Property(e => e.DuyuruTipi)
                   .HasDefaultValue(1); // 1: Genel

            builder.Property(e => e.AktifMi)
                   .HasDefaultValue(true);

            builder.Property(e => e.OkunduBilgisiToplansin)
                   .HasDefaultValue(false);

            builder.Property(e => e.GoruntulenmeSayisi)
                   .HasDefaultValue(0);

            // Tarih alanları
            builder.Property(e => e.YayinBaslangic)
                   .HasColumnType("datetime2")
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.YayinBitis)
                   .HasColumnType("datetime2");

            builder.Property(e => e.OlusturmaTarihi)
                   .HasColumnType("datetime2")
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.GuncellenmeTarihi)
                   .HasColumnType("datetime2");

            // İndeksler
            builder.HasIndex(e => e.OlusturanKullaniciId)
                   .HasName("IX_Duyuru_OlusturanKullaniciId");

            builder.HasIndex(e => e.HedefSubeId)
                   .HasName("IX_Duyuru_HedefSubeId");

            builder.HasIndex(e => e.DuyuruTipi)
                   .HasName("IX_Duyuru_DuyuruTipi");
        }
    }
}
