using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Mapping
{
    public class DepoTableMap : IEntityTypeConfiguration<DepoTable>
    {
        public void Configure(EntityTypeBuilder<DepoTable> builder)
        {
            // **Primary Key**
            builder.HasKey(e => e.Id);

            // **Zorunlu alanlar ve uzunluk sýnýrlarý**
            builder.Property(e => e.DepoKodu).IsRequired().HasMaxLength(20);
            builder.Property(e => e.DepoAdi).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Adres).IsRequired().HasMaxLength(250);

            // **Ýsteðe baðlý alanlar**
            builder.Property(e => e.YetkiliKodu).HasMaxLength(50);
            builder.Property(e => e.YetkiliAdi).HasMaxLength(100);
            builder.Property(e => e.Il).HasMaxLength(50);
            builder.Property(e => e.Ilce).HasMaxLength(50);
            builder.Property(e => e.Semt).HasMaxLength(50);
            builder.Property(e => e.Telefon).HasMaxLength(15);
            builder.Property(e => e.Aciklama).HasMaxLength(500);
        }
    }
}
