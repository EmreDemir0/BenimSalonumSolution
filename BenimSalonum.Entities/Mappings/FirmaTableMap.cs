using BenimSalonum.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BenimSalonum.Entities.Mappings
{
    public class FirmaTableMap : IEntityTypeConfiguration<FirmaTable>
    {
        public void Configure(EntityTypeBuilder<FirmaTable> builder)
        {
            // Tablo adı belirtme
            builder.ToTable("Firmalar");

            // Primary key tanımlama
            builder.HasKey(x => x.Id);

            // Alan özellikleri
            builder.Property(x => x.FirmaAdi).IsRequired().HasMaxLength(100);
            builder.Property(x => x.VergiNo).HasMaxLength(11);
            builder.Property(x => x.VergiDairesi).HasMaxLength(50);
            builder.Property(x => x.MersisNo).HasMaxLength(20);
            builder.Property(x => x.Adres).HasMaxLength(250);
            builder.Property(x => x.Il).HasMaxLength(50);
            builder.Property(x => x.Ilce).HasMaxLength(50);
            builder.Property(x => x.Telefon).HasMaxLength(20);
            builder.Property(x => x.Email).HasMaxLength(100);
            builder.Property(x => x.WebSitesi).HasMaxLength(100);
            builder.Property(x => x.OlusturulmaTarihi).IsRequired();

            // İlişkiler
            builder.HasMany(x => x.Kullanicilar)
                .WithOne(k => k.Firma)
                .HasForeignKey(k => k.FirmaId)
                .OnDelete(DeleteBehavior.Restrict); // Firma silindiğinde kullanıcılar silinmez
        }
    }
}
