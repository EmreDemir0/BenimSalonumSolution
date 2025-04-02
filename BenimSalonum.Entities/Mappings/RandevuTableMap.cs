using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Mapping
{
    public class RandevuTableMap : IEntityTypeConfiguration<RandevuTable>
    {
        public void Configure(EntityTypeBuilder<RandevuTable> builder)
        {
            // Primary Key
            builder.HasKey(e => e.Id);

            // Foreign Keys ve İlişkiler
            builder.HasIndex(e => e.MusteriId)
                   .HasName("IX_Randevu_MusteriId");

            builder.HasIndex(e => e.PersonelId)
                   .HasName("IX_Randevu_PersonelId");
                   
            builder.HasIndex(e => e.SubeId)
                   .HasName("IX_Randevu_SubeId");
                   
            builder.HasIndex(e => e.HizmetId)
                   .HasName("IX_Randevu_HizmetId");

            // İlişkileri tanımlama
            builder.HasOne<CariTable>()
                   .WithMany()
                   .HasForeignKey(e => e.MusteriId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<PersonelTable>()
                   .WithMany()
                   .HasForeignKey(e => e.PersonelId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Zorunlu alanlar
            builder.Property(e => e.MusteriId)
                   .IsRequired();

            builder.Property(e => e.PersonelId)
                   .IsRequired();

            builder.Property(e => e.SubeId)
                   .IsRequired();

            builder.Property(e => e.HizmetId)
                   .IsRequired();

            builder.Property(e => e.BaslangicTarihi)
                   .IsRequired()
                   .HasColumnType("datetime2");

            builder.Property(e => e.BitisTarihi)
                   .IsRequired()
                   .HasColumnType("datetime2");

            // İsteğe bağlı alanlar
            builder.Property(e => e.Notlar)
                   .HasMaxLength(500);

            // Varsayılan değerler
            builder.Property(e => e.Durum)
                   .HasDefaultValue(1);

            builder.Property(e => e.SmsGonderildi)
                   .HasDefaultValue(false);

            builder.Property(e => e.EmailGonderildi)
                   .HasDefaultValue(false);

            builder.Property(e => e.Tutar)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);

            builder.Property(e => e.IndirimTutari)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);

            builder.Property(e => e.OdemeYapildi)
                   .HasDefaultValue(false);

            builder.Property(e => e.OdemeTuruId)
                   .HasDefaultValue(0);

            builder.Property(e => e.OlusturmaTarihi)
                   .HasColumnType("datetime2")
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.OlusturanKullaniciId)
                   .IsRequired();

            builder.Property(e => e.GuncellenmeTarihi)
                   .HasColumnType("datetime2");
        }
    }
}
