using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Mapping
{
    public class SubeTableMap : IEntityTypeConfiguration<SubeTable>
    {
        public void Configure(EntityTypeBuilder<SubeTable> builder)
        {
            // Primary Key
            builder.HasKey(e => e.Id);
            
            // Zorunlu alanlar
            builder.Property(e => e.SubeAdi)
                   .IsRequired()
                   .HasMaxLength(100);
                   
            // Benzersiz indeks
            builder.HasIndex(e => e.SubeAdi)
                   .HasName("IX_Sube_SubeAdi")
                   .IsUnique();
                   
            // İsteğe bağlı alanlar
            builder.Property(e => e.Adres)
                   .HasMaxLength(200);
                   
            builder.Property(e => e.Telefon)
                   .HasMaxLength(20);
                   
            builder.Property(e => e.Email)
                   .HasMaxLength(100);
                   
            builder.Property(e => e.WebSite)
                   .HasMaxLength(100);
                   
            builder.Property(e => e.VergiDairesi)
                   .HasMaxLength(20);
                   
            builder.Property(e => e.VergiNo)
                   .HasMaxLength(20);
                   
            builder.Property(e => e.LisansKodu)
                   .HasMaxLength(100);
                   
            builder.Property(e => e.LogoUrl)
                   .HasMaxLength(500);
                   
            // Varsayılan değerler
            builder.Property(e => e.AktifMi)
                   .HasDefaultValue(true);
                   
            builder.Property(e => e.KullaniciLimiti)
                   .HasDefaultValue(5);
                   
            builder.Property(e => e.EFaturaKullanimi)
                   .HasDefaultValue(false);
                   
            builder.Property(e => e.WhatsappKullanimi)
                   .HasDefaultValue(false);
                   
            builder.Property(e => e.SmsKullanimi)
                   .HasDefaultValue(false);
                   
            builder.Property(e => e.EMailKullanimi)
                   .HasDefaultValue(false);
                   
            builder.Property(e => e.EticaretEntegrasyonu)
                   .HasDefaultValue(false);
                   
            // Tarih alanları
            builder.Property(e => e.LisansBitisTarihi)
                   .HasColumnType("datetime2");
                   
            builder.Property(e => e.OlusturmaTarihi)
                   .HasColumnType("datetime2")
                   .HasDefaultValueSql("GETDATE()");
                   
            builder.Property(e => e.GuncellenmeTarihi)
                   .HasColumnType("datetime2");
                   
            // İndeksler
            builder.HasIndex(e => e.AktifMi)
                   .HasName("IX_Sube_AktifMi");
        }
    }
}
