using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Mapping
{
    public class SiparisTableMap : IEntityTypeConfiguration<SiparisTable>
    {
        public void Configure(EntityTypeBuilder<SiparisTable> builder)
        {
            // Primary Key
            builder.HasKey(e => e.Id);
            
            // Zorunlu alanlar
            builder.Property(e => e.SubeId)
                   .IsRequired();
                   
            builder.Property(e => e.SiparisTuru)
                   .IsRequired();
                   
            builder.Property(e => e.SiparisNo)
                   .IsRequired()
                   .HasMaxLength(20);
                   
            builder.Property(e => e.CariId)
                   .IsRequired();
                   
            builder.Property(e => e.SiparisTarihi)
                   .IsRequired()
                   .HasColumnType("datetime2");
                   
            // İsteğe bağlı alanlar
            builder.Property(e => e.TeslimTarihi)
                   .HasColumnType("datetime2");
                   
            builder.Property(e => e.EticaretSiparisNo)
                   .HasMaxLength(100);
                   
            builder.Property(e => e.Aciklama)
                   .HasMaxLength(500);
                   
            // Durum alanları
            builder.Property(e => e.SiparisDurumu)
                   .HasDefaultValue(1);
                   
            builder.Property(e => e.OnayDurumu)
                   .HasDefaultValue(1);
                   
            builder.Property(e => e.EticaretPlatformu)
                   .HasDefaultValue(0);
                   
            builder.Property(e => e.FaturaKesildi)
                   .HasDefaultValue(false);
                   
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
                   
            builder.Property(e => e.KargoTutar)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);
                   
            builder.Property(e => e.GenelToplam)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);
                   
            // Kargo bilgileri
            builder.Property(e => e.KargoSirketi)
                   .HasMaxLength(50);
                   
            builder.Property(e => e.KargoTakipNo)
                   .HasMaxLength(50);
                   
            builder.Property(e => e.KargoTarihi)
                   .HasColumnType("datetime2");
                   
            // Müşteri bilgileri
            builder.Property(e => e.CariUnvan)
                   .HasMaxLength(100);
                   
            builder.Property(e => e.TeslimatAdresi)
                   .HasMaxLength(200);
                   
            builder.Property(e => e.TeslimatIl)
                   .HasMaxLength(30);
                   
            builder.Property(e => e.TeslimatIlce)
                   .HasMaxLength(30);
                   
            builder.Property(e => e.TelefonNo)
                   .HasMaxLength(20);
                   
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
            builder.HasIndex(e => new { e.SiparisNo, e.SubeId })
                   .HasName("IX_Siparis_SiparisNo_SubeId")
                   .IsUnique();
                   
            builder.HasIndex(e => e.CariId)
                   .HasName("IX_Siparis_CariId");
                   
            builder.HasIndex(e => e.SiparisTarihi)
                   .HasName("IX_Siparis_SiparisTarihi");
                   
            builder.HasIndex(e => e.SiparisDurumu)
                   .HasName("IX_Siparis_SiparisDurumu");
                   
            builder.HasIndex(e => e.OnayDurumu)
                   .HasName("IX_Siparis_OnayDurumu");
                   
            builder.HasIndex(e => e.EticaretPlatformu)
                   .HasName("IX_Siparis_EticaretPlatformu");
                   
            builder.HasIndex(e => e.EticaretSiparisNo)
                   .HasName("IX_Siparis_EticaretSiparisNo");
                   
            builder.HasIndex(e => e.FaturaId)
                   .HasName("IX_Siparis_FaturaId");
                   
            builder.HasIndex(e => e.MaliYil)
                   .HasName("IX_Siparis_MaliYil");
                   
            // İlişkiler
            builder.HasOne(s => s.Cari)
                   .WithMany(c => c.Siparisler)
                   .HasForeignKey(s => s.CariId)
                   .OnDelete(DeleteBehavior.Restrict);
                   
            builder.HasOne(s => s.Sube)
                   .WithMany(s => s.Siparisler)
                   .HasForeignKey(s => s.SubeId)
                   .OnDelete(DeleteBehavior.Restrict);
                   
            // Sipariş ile Fatura arasındaki bire-bir ilişki (Dependent)
            builder.HasOne(s => s.Fatura)
                   .WithOne(f => f.Siparis)
                   .HasForeignKey<SiparisTable>(s => s.FaturaId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
