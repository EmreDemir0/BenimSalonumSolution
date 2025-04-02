using BenimSalonum.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BenimSalonum.Entities.Mappings
{
    public class LisansTableMap : IEntityTypeConfiguration<LisansTable>
    {
        public void Configure(EntityTypeBuilder<LisansTable> builder)
        {
            // Tablo adı belirtme
            builder.ToTable("Lisanslar");

            // Primary key tanımlama
            builder.HasKey(x => x.Id);

            // Alan özellikleri
            builder.Property(x => x.LisansKodu).IsRequired().HasMaxLength(100);
            builder.Property(x => x.FirmaId).IsRequired();
            builder.Property(x => x.BaslangicTarihi).IsRequired();
            builder.Property(x => x.BitisTarihi).IsRequired();
            builder.Property(x => x.KullaniciSayisiLimiti).IsRequired();
            builder.Property(x => x.Aktif).IsRequired();
            builder.Property(x => x.AktivasyonAnahtari).HasMaxLength(250);
            builder.Property(x => x.LisansTuru).HasMaxLength(50);
            builder.Property(x => x.Notlar).HasMaxLength(250);

            // İlişkiler
            builder.HasOne(x => x.Firma)
                .WithMany()
                .HasForeignKey(x => x.FirmaId)
                .OnDelete(DeleteBehavior.Restrict); // Firma silindiğinde lisans silinmez
        }
    }
}
