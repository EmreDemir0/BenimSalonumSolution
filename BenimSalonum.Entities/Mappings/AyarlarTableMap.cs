using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Mapping
{
    public class AyarlarTableMap : IEntityTypeConfiguration<AyarlarTable>
    {
        public void Configure(EntityTypeBuilder<AyarlarTable> builder)
        {
            // Primary Key
            builder.HasKey(e => e.Id);
            
            // Bileşik indeks: Şube ve ayar adına göre tekil olmalı
            builder.HasIndex(e => new { e.SubeId, e.AyarAdi })
                   .HasName("IX_Ayarlar_SubeId_AyarAdi")
                   .IsUnique();
                   
            // İndeksler
            builder.HasIndex(e => e.AyarGrubu)
                   .HasName("IX_Ayarlar_AyarGrubu");
                   
            // Zorunlu alanlar
            builder.Property(e => e.AyarAdi)
                   .IsRequired()
                   .HasMaxLength(100);
                   
            builder.Property(e => e.AyarGrubu)
                   .IsRequired()
                   .HasMaxLength(50);
                   
            builder.Property(e => e.AyarDegeri)
                   .IsRequired()
                   .HasMaxLength(1000);
                   
            // İsteğe bağlı alanlar
            builder.Property(e => e.Aciklama)
                   .HasMaxLength(500);
                   
            // SMS ayarları
            builder.Property(e => e.SmsApiKey)
                   .HasMaxLength(100);
                   
            builder.Property(e => e.SmsApiSecret)
                   .HasMaxLength(100);
                   
            builder.Property(e => e.SmsApiUrl)
                   .HasMaxLength(100);
                   
            builder.Property(e => e.SmsTitleId)
                   .HasMaxLength(30);
                   
            // Mail ayarları
            builder.Property(e => e.MailHost)
                   .HasMaxLength(100);
                   
            builder.Property(e => e.MailKullanici)
                   .HasMaxLength(100);
                   
            builder.Property(e => e.MailSifre)
                   .HasMaxLength(200);
                   
            builder.Property(e => e.MailGonderenAdi)
                   .HasMaxLength(100);
                   
            // E-Fatura ayarları
            builder.Property(e => e.EFaturaEntegratorUrl)
                   .HasMaxLength(200);
                   
            builder.Property(e => e.EFaturaKullanici)
                   .HasMaxLength(100);
                   
            builder.Property(e => e.EFaturaSifre)
                   .HasMaxLength(200);
                   
            // WhatsApp ayarları
            builder.Property(e => e.WhatsappApiKey)
                   .HasMaxLength(200);
                   
            builder.Property(e => e.WhatsappInstanceId)
                   .HasMaxLength(100);
                   
            builder.Property(e => e.WhatsappPhoneNumber)
                   .HasMaxLength(100);
                   
            // Varsayılan değerler
            builder.Property(e => e.SubeId)
                   .HasDefaultValue(0);
                   
            builder.Property(e => e.SiraNo)
                   .HasDefaultValue(0);
                   
            builder.Property(e => e.AktifMi)
                   .HasDefaultValue(true);
                   
            // Tarih alanları
            builder.Property(e => e.OlusturmaTarihi)
                   .HasColumnType("datetime2")
                   .HasDefaultValueSql("GETDATE()");
                   
            builder.Property(e => e.GuncellenmeTarihi)
                   .HasColumnType("datetime2");
        }
    }
}
