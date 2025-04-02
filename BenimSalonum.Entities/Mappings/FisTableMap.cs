using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Mapping
{
    public class FisTableMap : IEntityTypeConfiguration<FisTable>
    {
        public void Configure(EntityTypeBuilder<FisTable> builder)
        {
            // **Primary Key**
            builder.HasKey(e => e.Id);

            // **Zorunlu alanlar ve uzunluk sýnýrlarý**
            builder.Property(e => e.FisKodu).IsRequired().HasMaxLength(20);
            builder.Property(e => e.FisTuru).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Tarih).IsRequired().HasColumnType("datetime2");

            // **Ýsteðe baðlý alanlar**
            builder.Property(e => e.CariId);
            builder.Property(e => e.FaturaUnvani).HasMaxLength(100);
            builder.Property(e => e.CepTelefonu).HasMaxLength(15);
            builder.Property(e => e.Il).HasMaxLength(50);
            builder.Property(e => e.Ilce).HasMaxLength(50);
            builder.Property(e => e.Semt).HasMaxLength(50);
            builder.Property(e => e.Adres).HasMaxLength(250);
            builder.Property(e => e.VergiDairesi).HasMaxLength(50);
            builder.Property(e => e.VergiNo).HasMaxLength(20);
            builder.Property(e => e.BelgeNo).HasMaxLength(30);
            builder.Property(e => e.PlasiyerId);
            builder.Property(e => e.IskontoOrani).HasColumnType("decimal(18,2)");
            builder.Property(e => e.IskontoTutar).HasColumnType("decimal(18,2)");
            builder.Property(e => e.Alacak).HasColumnType("decimal(18,2)");
            builder.Property(e => e.Borc).HasColumnType("decimal(18,2)");
            builder.Property(e => e.ToplamTutar).HasColumnType("decimal(18,2)");
            builder.Property(e => e.Aciklama).HasMaxLength(500);
            builder.Property(e => e.FisBaglantiKodu).HasMaxLength(30);
        }
    }
}
