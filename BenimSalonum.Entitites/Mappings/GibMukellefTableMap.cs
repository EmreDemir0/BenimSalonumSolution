using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Mappings
{
    public class GibMukellefTableMap : IEntityTypeConfiguration<GibMukellefTable>
    {
        public void Configure(EntityTypeBuilder<GibMukellefTable> builder)
        {
            // Tablo adı
            builder.ToTable("GibMukellefler");
            
            // Primary Key
            builder.HasKey(e => e.Id);
            
            // Alanlar
            builder.Property(e => e.VKN_TCKN)
                   .HasMaxLength(11)
                   .IsRequired();
                   
            builder.Property(e => e.Unvan)
                   .HasMaxLength(100)
                   .IsRequired(false);
                   
            builder.Property(e => e.EFaturaKullanicisi)
                   .IsRequired();
                   
            builder.Property(e => e.EIrsaliyeKullanicisi)
                   .IsRequired();
                   
            builder.Property(e => e.SorgulamaTarihi)
                   .HasColumnType("datetime2")
                   .IsRequired();
                   
            builder.Property(e => e.GecerlilikTarihi)
                   .HasColumnType("datetime2")
                   .IsRequired(false);
                   
            builder.Property(e => e.PostaKutusu)
                   .HasMaxLength(100)
                   .IsRequired(false);
                   
            builder.Property(e => e.Etiket)
                   .HasMaxLength(100)
                   .IsRequired(false);
                   
            builder.Property(e => e.XmlYanit)
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
                   
            // İndeksler
            builder.HasIndex(e => e.VKN_TCKN).IsUnique();
            builder.HasIndex(e => e.SorgulamaTarihi);
            builder.HasIndex(e => e.GecerlilikTarihi);
        }
    }
}
