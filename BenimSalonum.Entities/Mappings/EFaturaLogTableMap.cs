using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Mapping
{
    public class EFaturaLogTableMap : IEntityTypeConfiguration<EFaturaLogTable>
    {
        public void Configure(EntityTypeBuilder<EFaturaLogTable> builder)
        {
            // Primary Key
            builder.HasKey(e => e.Id);
            
            // Foreign Keys
            builder.HasIndex(e => e.FaturaId)
                   .HasName("IX_EFaturaLog_FaturaId");
                   
            // Zorunlu alanlar
            builder.Property(e => e.FaturaId)
                   .IsRequired();
                   
            builder.Property(e => e.IslemTuru)
                   .IsRequired();
                   
            builder.Property(e => e.IslemDurumu)
                   .IsRequired();
                   
            builder.Property(e => e.IslemTarihi)
                   .IsRequired()
                   .HasColumnType("datetime2")
                   .HasDefaultValueSql("GETDATE()");
                   
            // İsteğe bağlı alanlar
            builder.Property(e => e.UUID)
                   .HasMaxLength(36);
                   
            builder.Property(e => e.BelgeNo)
                   .HasMaxLength(50);
                   
            builder.Property(e => e.RequestData)
                   .HasMaxLength(500);
                   
            builder.Property(e => e.ResponseData)
                   .HasMaxLength(500);
                   
            builder.Property(e => e.HataMesaji)
                   .HasMaxLength(500);
                   
            builder.Property(e => e.PdfUrl)
                   .HasMaxLength(500);
                   
            builder.Property(e => e.XmlUrl)
                   .HasMaxLength(500);
                   
            builder.Property(e => e.ZrfUrl)
                   .HasMaxLength(500);
                   
            builder.Property(e => e.EttnNo)
                   .HasMaxLength(100);
                   
            builder.Property(e => e.MailGonderimTarihi)
                   .HasColumnType("datetime2");
                   
            builder.Property(e => e.SmsGonderimTarihi)
                   .HasColumnType("datetime2");
                   
            builder.Property(e => e.MailAdresi)
                   .HasMaxLength(100);
                   
            builder.Property(e => e.TelefonNo)
                   .HasMaxLength(20);
                   
            builder.Property(e => e.Aciklama)
                   .HasMaxLength(500)
                   .IsRequired(false);
                   
            // WSDL için yeni eklenen alanlar
            builder.Property(e => e.EnvelopeId)
                   .HasMaxLength(100)
                   .IsRequired(false);
                   
            builder.Property(e => e.EarsivRaporDurumu)
                   .IsRequired(false);
                   
            builder.Property(e => e.GibMesajId)
                   .HasMaxLength(100)
                   .IsRequired(false);
                   
            builder.Property(e => e.KontorMiktari)
                   .IsRequired(false);
                   
            // İndeksler
            builder.HasIndex(e => e.IslemTuru)
                   .HasName("IX_EFaturaLog_IslemTuru");
                   
            builder.HasIndex(e => e.IslemDurumu)
                   .HasName("IX_EFaturaLog_IslemDurumu");
                   
            builder.HasIndex(e => e.UUID)
                   .HasName("IX_EFaturaLog_UUID");
                   
            builder.HasIndex(e => e.IslemTarihi)
                   .HasName("IX_EFaturaLog_IslemTarihi");
                   
            // İlişkiler
            builder.HasOne<FaturaTable>()
                   .WithMany()
                   .HasForeignKey(e => e.FaturaId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
