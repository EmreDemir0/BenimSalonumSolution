using BenimSalonum.Entities.Tables;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class KasaTableMap : IEntityTypeConfiguration<KasaTable>
{
 public void Configure(EntityTypeBuilder<KasaTable> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.KasaKodu)
               .IsRequired()
               .HasMaxLength(30);

        builder.Property(e => e.KasaAdi)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(e => e.YetkiliKodu)
               .HasMaxLength(50);

        builder.Property(e => e.YetkiliAdi)
               .HasMaxLength(100);

        builder.Property(e => e.Aciklama)
               .HasMaxLength(500);


    }

}
