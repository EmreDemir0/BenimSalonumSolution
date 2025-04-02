using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Mapping
{
    public class StokHareketTableMap : IEntityTypeConfiguration<StokHareketTable>
    {
        public void Configure(EntityTypeBuilder<StokHareketTable> builder)
        {
            // **Primary Key**
            builder.HasKey(e => e.Id); // Id alaný primary key olarak belirleniyor.

            // **Zorunlu alanlar ve uzunluk sýnýrlarý**
            builder.Property(e => e.FisKodu)
                   .IsRequired() // FisKodu zorunlu
                   .HasMaxLength(20); // FisKodu'nun maksimum uzunluðu 20 karakter olacak

            builder.Property(e => e.Hareket)
                   .IsRequired() // Hareket zorunlu
                   .HasMaxLength(50); // Hareket türünün maksimum uzunluðu 50 karakter olacak

            builder.Property(e => e.Kdv)
                   .IsRequired(); // KDV zorunlu, ancak herhangi bir uzunluk sýnýrý yok

            builder.Property(e => e.DepoId)
                   .IsRequired(); // DepoId zorunlu (foreign key)

            // **Ýsteðe baðlý alanlar (nullable)**
            builder.Property(e => e.Miktar)
                   .HasColumnType("decimal(18,3)"); // Miktar, decimal(18,3) formatýnda

            builder.Property(e => e.BirimFiyati)
                   .HasColumnType("decimal(18,2)"); // BirimFiyati, decimal(18,2) formatýnda

            builder.Property(e => e.IndirimOrani)
                   .HasColumnType("decimal(5,2)"); // IndirimOrani, decimal(5,2) formatýnda

            builder.Property(e => e.SeriNo)
                   .HasMaxLength(50); // SeriNo, isteðe baðlý, maksimum uzunluk 50 karakter

            builder.Property(e => e.Tarih)
                   .HasColumnType("datetime2") // Tarih, datetime2 formatýnda
                   .HasDefaultValueSql("GETDATE()"); // Varsayýlan olarak þu anki tarih

            builder.Property(e => e.Aciklama)
                   .HasMaxLength(500); // Aciklama, isteðe baðlý, maksimum uzunluk 500 karakter

            builder.Property(e => e.Siparis)
                   .HasDefaultValue(false); // Varsayýlan olarak Siparis false (sipariþ deðil)

            // **Navigation Properties (Foreign Key iliþkileri)**
            builder.HasOne(e => e.Stok) // Stok ile iliþki
                   .WithMany() // Bir stok çoklu hareketlere sahip olabilir
                   .HasForeignKey(e => e.StokId)
                   .OnDelete(DeleteBehavior.Restrict); // Silme iþleminde kýsýtlama yapýlýr

            builder.HasOne(e => e.Depo) // Depo ile iliþki
                   .WithMany() // Bir depo çoklu hareketlere sahip olabilir
                   .HasForeignKey(e => e.DepoId)
                   .OnDelete(DeleteBehavior.Restrict); // Silme iþleminde kýsýtlama yapýlýr
        }
    }
}
