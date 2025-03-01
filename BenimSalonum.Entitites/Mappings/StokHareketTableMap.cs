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
            builder.HasKey(e => e.Id); // Id alan� primary key olarak belirleniyor.

            // **Zorunlu alanlar ve uzunluk s�n�rlar�**
            builder.Property(e => e.FisKodu)
                   .IsRequired() // FisKodu zorunlu
                   .HasMaxLength(20); // FisKodu'nun maksimum uzunlu�u 20 karakter olacak

            builder.Property(e => e.Hareket)
                   .IsRequired() // Hareket zorunlu
                   .HasMaxLength(50); // Hareket t�r�n�n maksimum uzunlu�u 50 karakter olacak

            builder.Property(e => e.Kdv)
                   .IsRequired(); // KDV zorunlu, ancak herhangi bir uzunluk s�n�r� yok

            builder.Property(e => e.DepoId)
                   .IsRequired(); // DepoId zorunlu (foreign key)

            // **�ste�e ba�l� alanlar (nullable)**
            builder.Property(e => e.Miktar)
                   .HasColumnType("decimal(18,3)"); // Miktar, decimal(18,3) format�nda

            builder.Property(e => e.BirimFiyati)
                   .HasColumnType("decimal(18,2)"); // BirimFiyati, decimal(18,2) format�nda

            builder.Property(e => e.IndirimOrani)
                   .HasColumnType("decimal(5,2)"); // IndirimOrani, decimal(5,2) format�nda

            builder.Property(e => e.SeriNo)
                   .HasMaxLength(50); // SeriNo, iste�e ba�l�, maksimum uzunluk 50 karakter

            builder.Property(e => e.Tarih)
                   .HasColumnType("datetime2") // Tarih, datetime2 format�nda
                   .HasDefaultValueSql("GETDATE()"); // Varsay�lan olarak �u anki tarih

            builder.Property(e => e.Aciklama)
                   .HasMaxLength(500); // Aciklama, iste�e ba�l�, maksimum uzunluk 500 karakter

            builder.Property(e => e.Siparis)
                   .HasDefaultValue(false); // Varsay�lan olarak Siparis false (sipari� de�il)

            // **Navigation Properties (Foreign Key ili�kileri)**
            builder.HasOne(e => e.Stok) // Stok ile ili�ki
                   .WithMany() // Bir stok �oklu hareketlere sahip olabilir
                   .HasForeignKey(e => e.StokId)
                   .OnDelete(DeleteBehavior.Restrict); // Silme i�leminde k�s�tlama yap�l�r

            builder.HasOne(e => e.Depo) // Depo ile ili�ki
                   .WithMany() // Bir depo �oklu hareketlere sahip olabilir
                   .HasForeignKey(e => e.DepoId)
                   .OnDelete(DeleteBehavior.Restrict); // Silme i�leminde k�s�tlama yap�l�r
        }
    }
}
