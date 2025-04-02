using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class KasaHareketTableMap : IEntityTypeConfiguration<KasaHareketTable>
{
    public void Configure(EntityTypeBuilder<KasaHareketTable> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.FisKodu)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(e => e.Hareket)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(e => e.KasaId)
               .IsRequired(); // Sadece Kasa ID'sini tutar, iliþki yok

        builder.Property(e => e.OdemeTuruId)
               .IsRequired(); // Sadece Ödeme Türü ID'sini tutar, iliþki yok

        builder.Property(e => e.Tarih)
               .IsRequired()
               .HasColumnType("datetime2");

        builder.Property(e => e.Tutar)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        builder.Property(e => e.CariId); // Opsiyonel
    }
}