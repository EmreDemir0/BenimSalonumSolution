using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Mapping
{
    public class FaturaTableMap : IEntityTypeConfiguration<FaturaTable>
    {
        public void Configure(EntityTypeBuilder<FaturaTable> builder)
        {
            // Primary Key
            builder.HasKey(e => e.Id);
            
            // Zorunlu alanlar
            builder.Property(e => e.SubeId)
                   .IsRequired();
                   
            builder.Property(e => e.FaturaTuru)
                   .IsRequired();
                   
            builder.Property(e => e.FaturaTipi)
                   .IsRequired();
                   
            builder.Property(e => e.FaturaNo)
                   .IsRequired()
                   .HasMaxLength(16);
                   
            builder.Property(e => e.BelgeNo)
                   .IsRequired()
                   .HasMaxLength(50);
                   
            builder.Property(e => e.CariId)
                   .IsRequired();
                   
            builder.Property(e => e.FaturaTarihi)
                   .IsRequired()
                   .HasColumnType("datetime2");
                   
            builder.Property(e => e.VadeTarihi)
                   .IsRequired()
                   .HasColumnType("datetime2");
                   
            // İsteğe bağlı alanlar
            builder.Property(e => e.EfaturaNo)
                   .HasMaxLength(50);
                   
            builder.Property(e => e.EarsivNo)
                   .HasMaxLength(50);
                   
            builder.Property(e => e.EirsaliyeNo)
                   .HasMaxLength(50);
                   
            builder.Property(e => e.SevkTarihi)
                   .HasColumnType("datetime2");
                   
            builder.Property(e => e.OdemeSekli)
                   .HasMaxLength(50);
                   
            builder.Property(e => e.Aciklama)
                   .HasMaxLength(500);
                   
            // Tutar alanları
            builder.Property(e => e.AraToplam)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);
                   
            builder.Property(e => e.KdvToplam)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);
                   
            builder.Property(e => e.IndirimTutar)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);
                   
            builder.Property(e => e.GenelToplam)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);
                   
            builder.Property(e => e.OdenenTutar)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);
                   
            builder.Property(e => e.KalanTutar)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);
                   
            // Durum alanları
            builder.Property(e => e.KdvDahil)
                   .HasDefaultValue(false);
                   
            builder.Property(e => e.FaturaDurumu)
                   .HasDefaultValue(1);
                   
            builder.Property(e => e.OdemeDurumu)
                   .HasDefaultValue(1);
                   
            // E-Fatura bilgileri
            builder.Property(e => e.EfaturaGuid)
                   .HasMaxLength(200);
                   
            builder.Property(e => e.EfaturaDurum)
                   .HasMaxLength(100);
                   
            builder.Property(e => e.EfaturaHata)
                   .HasMaxLength(500);
                   
            builder.Property(e => e.EfaturaGonderimTarihi)
                   .HasColumnType("datetime2");
                   
            builder.Property(e => e.EfaturaBasarili)
                   .HasDefaultValue(false);
                   
            builder.Property(e => e.EfaturaPdf)
                   .HasMaxLength(500);
                   
            builder.Property(e => e.EfaturaXml)
                   .HasMaxLength(500);
                   
            // E-Fatura entegrasyon alanları
            builder.Property(e => e.GibFaturaDurumu).HasMaxLength(50).IsRequired(false);
            builder.Property(e => e.PostaKutusuId).HasMaxLength(100).IsRequired(false);
            builder.Property(e => e.EtiketId).HasMaxLength(100).IsRequired(false);
            builder.Property(e => e.EarsivRaporId).IsRequired(false);
            builder.Property(e => e.KullanılanKontorAdet).IsRequired(false);

            // Cari bilgileri
            builder.Property(e => e.CariUnvan)
                   .HasMaxLength(100);
                   
            builder.Property(e => e.CariTckn)
                   .HasMaxLength(11);
                   
            builder.Property(e => e.CariVkn)
                   .HasMaxLength(11);
                   
            builder.Property(e => e.CariVergiDairesi)
                   .HasMaxLength(30);
                   
            builder.Property(e => e.CariAdres)
                   .HasMaxLength(200);
                   
            builder.Property(e => e.CariIl)
                   .HasMaxLength(30);
                   
            builder.Property(e => e.CariIlce)
                   .HasMaxLength(30);
                   
            // İzleme bilgileri
            builder.Property(e => e.OlusturmaTarihi)
                   .HasColumnType("datetime2")
                   .HasDefaultValueSql("GETDATE()");
                   
            builder.Property(e => e.GuncellenmeTarihi)
                   .HasColumnType("datetime2");
                   
            builder.Property(e => e.OnayTarihi)
                   .HasColumnType("datetime2");
                   
            builder.Property(e => e.IptalTarihi)
                   .HasColumnType("datetime2");
                   
            builder.Property(e => e.IptalNedeni)
                   .HasMaxLength(500);
                   
            // İndeksler
            builder.HasIndex(e => new { e.FaturaNo, e.SubeId })
                   .HasName("IX_Fatura_FaturaNo_SubeId")
                   .IsUnique();
                   
            builder.HasIndex(e => e.CariId)
                   .HasName("IX_Fatura_CariId");
                   
            builder.HasIndex(e => e.FaturaTarihi)
                   .HasName("IX_Fatura_FaturaTarihi");
                   
            builder.HasIndex(e => e.VadeTarihi)
                   .HasName("IX_Fatura_VadeTarihi");
                   
            builder.HasIndex(e => e.FaturaDurumu)
                   .HasName("IX_Fatura_FaturaDurumu");
                   
            builder.HasIndex(e => e.OdemeDurumu)
                   .HasName("IX_Fatura_OdemeDurumu");
                   
            builder.HasIndex(e => new { e.MaliYil, e.MaliDonem })
                   .HasName("IX_Fatura_MaliYil_MaliDonem");
                   
            // İlişkiler
            builder.HasOne(f => f.Cari)
                   .WithMany(c => c.Faturalar)
                   .HasForeignKey(f => f.CariId)
                   .OnDelete(DeleteBehavior.Restrict);
                   
            builder.HasOne(f => f.Sube)
                   .WithMany(s => s.Faturalar)
                   .HasForeignKey(f => f.SubeId)
                   .OnDelete(DeleteBehavior.Restrict);
                   
            // Fatura ile Sipariş arasındaki bire-bir ilişki (Principal)
            builder.HasOne(f => f.Siparis)
                   .WithOne(s => s.Fatura)
                   .HasForeignKey<SiparisTable>(s => s.FaturaId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
