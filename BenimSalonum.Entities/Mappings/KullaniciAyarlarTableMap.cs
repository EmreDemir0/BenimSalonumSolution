using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Mappings
{
    public class KullaniciAyarlarTableMap : IEntityTypeConfiguration<KullaniciAyarlarTable>
    {
        public void Configure(EntityTypeBuilder<KullaniciAyarlarTable> builder)
        {
            builder.ToTable("KullaniciAyarlar");
            
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            
            builder.Property(x => x.KullaniciId).IsRequired();
            
            // Bildirim ayarları
            builder.Property(x => x.EmailBildirimAktif).HasDefaultValue(true);
            builder.Property(x => x.SMSBildirimAktif).HasDefaultValue(true);
            builder.Property(x => x.UygulamaIciBildirimAktif).HasDefaultValue(true);
            
            // Randevu hatırlatma ayarları
            builder.Property(x => x.RandevuHatirlatmaZamani).HasDefaultValue(60);
            
            // Arayüz ayarları
            builder.Property(x => x.Dil).HasMaxLength(30).HasDefaultValue("tr-TR");
            builder.Property(x => x.Tema).HasMaxLength(30).HasDefaultValue("Light");
            
            // Çalışma takvimi ayarları
            builder.Property(x => x.CalismaBaslangicSaati).HasMaxLength(10).HasDefaultValue("09:00");
            builder.Property(x => x.CalismaBitisSaati).HasMaxLength(10).HasDefaultValue("18:00");
            
            // Güvenlik ayarları
            builder.Property(x => x.OturumSuresi).HasDefaultValue(120);
            builder.Property(x => x.OtomatikKilitlemeAktif).HasDefaultValue(false);
            builder.Property(x => x.OtomatikKilitlemeSuresi).HasDefaultValue(15);
            
            // İlişki tanımlama
            builder.HasOne(x => x.Kullanici)
                   .WithMany()
                   .HasForeignKey(x => x.KullaniciId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
