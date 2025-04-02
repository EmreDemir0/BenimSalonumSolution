using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Mapping
{
    public class KullaniciRolTableMap : IEntityTypeConfiguration<KullaniciRolTable>
    {
        public void Configure(EntityTypeBuilder<KullaniciRolTable> builder)
        {
            // **Primary Key**
            builder.HasKey(e => e.Id); // Id alaný primary key olarak belirleniyor.

            // **Zorunlu alanlar ve uzunluk sýnýrlarý**
            builder.Property(e => e.KullaniciAdi)
                   .IsRequired() // KullaniciAdi zorunlu
                   .HasMaxLength(50); // KullaniciAdi'nýn maksimum uzunluðu 50 karakter olacak

            builder.Property(e => e.FormAdi)
                   .IsRequired() // FormAdi zorunlu
                   .HasMaxLength(100); // FormAdi'nýn maksimum uzunluðu 100 karakter olacak

            builder.Property(e => e.KontrolAdi)
                   .IsRequired() // KontrolAdi zorunlu
                   .HasMaxLength(100); // KontrolAdi'nýn maksimum uzunluðu 100 karakter olacak

            builder.Property(e => e.RootId)
                   .IsRequired(); // RootId zorunlu

            builder.Property(e => e.ParentId)
                   .IsRequired(); // ParentId zorunlu

            builder.Property(e => e.Yetki)
                   .HasDefaultValue(false); // Varsayýlan olarak Yetki false (yetkisiz)
        }
    }
}
