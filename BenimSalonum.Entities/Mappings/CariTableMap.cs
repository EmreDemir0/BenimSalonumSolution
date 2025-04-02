using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;
using System;

namespace BenimSalonum.Entities.Mapping
{
    public class CariTableMap : IEntityTypeConfiguration<CariTable>
    {
        public void Configure(EntityTypeBuilder<CariTable> builder)
        {
            // **Primary Key**
            builder.HasKey(e => e.Id);

            // **Varsayýlan deðerler**
            builder.Property(e => e.Durumu).HasDefaultValue(true);

            // **Zorunlu alanlar ve uzunluk sýnýrlarý**
            builder.Property(e => e.CariTuru).IsRequired().HasMaxLength(50);
            builder.Property(e => e.CariKodu).IsRequired().HasMaxLength(20);
            builder.Property(e => e.CariAdi).IsRequired().HasMaxLength(100);
            builder.Property(e => e.FaturaUnvani).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Il).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Adres).IsRequired().HasMaxLength(250);

            // **Ýsteðe baðlý alanlar**
            builder.Property(e => e.YetkiliKisi).HasMaxLength(50);
            builder.Property(e => e.CepTelefonu).HasMaxLength(15);
            builder.Property(e => e.Telefon).HasMaxLength(15);
            builder.Property(e => e.Fax).HasMaxLength(15);
            builder.Property(e => e.EMail).HasMaxLength(100);
            builder.Property(e => e.Web).HasMaxLength(150);
            builder.Property(e => e.Ilce).HasMaxLength(50);
            builder.Property(e => e.Semt).HasMaxLength(50);
            builder.Property(e => e.CariGrubu).HasMaxLength(50);
            builder.Property(e => e.CariAltGrubu).HasMaxLength(50);
            builder.Property(e => e.OzelKod1).HasMaxLength(30);
            builder.Property(e => e.OzelKod2).HasMaxLength(30);
            builder.Property(e => e.OzelKod3).HasMaxLength(30);
            builder.Property(e => e.OzelKod4).HasMaxLength(30);
            builder.Property(e => e.VergiDairesi).HasMaxLength(50);
            builder.Property(e => e.VergiNo).HasMaxLength(20);
            builder.Property(e => e.Aciklama).HasMaxLength(500);

            // Eðer nullable decimal (decimal?) ise, varsayýlan deðeri null olmalýdýr
            builder.Property(x => x.IskontoOrani)
                .HasDefaultValue(null) // Nullable decimal için null deðeri
                .IsRequired(false);  // Ýhtiyaç duyuluyorsa nullable olmalý

            builder.Property(e => e.RiskLimiti)
     .HasColumnType("decimal(18,2)")   // Veritabaný türü
     .HasDefaultValue(null)             // Varsayýlan deðer null olacak
     .IsRequired(false);                // Nullable olduðu için false


            // **Tarih Alaný**
            builder.Property(e => e.KayitTarihi)
                   .HasColumnType("datetime2")
                   .HasDefaultValueSql("GETDATE()");
        }
    }
}
