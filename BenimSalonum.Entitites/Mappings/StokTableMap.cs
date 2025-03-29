using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Mapping
{
    public class StokTableMap : IEntityTypeConfiguration<StokTable>
    {
        public void Configure(EntityTypeBuilder<StokTable> builder)
        {
            // **Primary Key**
            builder.HasKey(e => e.Id); // Id alanı primary key olarak belirleniyor.

            // **Varsayılan değerler**
            builder.Property(e => e.Durumu)
                   .HasDefaultValue(true); // Varsayılan olarak aktif stok

            // **Zorunlu alanlar ve uzunluk sınırları**
            builder.Property(e => e.StokKodu)
                   .IsRequired() // StokKodu zorunlu
                   .HasMaxLength(30); // StokKodu'nun maksimum uzunluğu 30 karakter olacak

            builder.Property(e => e.StokAdi)
                   .IsRequired() // StokAdi zorunlu
                   .HasMaxLength(100); // StokAdi'nin maksimum uzunluğu 100 karakter olacak

            builder.Property(e => e.Birimi)
                   .IsRequired() // Birimi zorunlu
                   .HasMaxLength(20); // Birimi'nin maksimum uzunluğu 20 karakter olacak

            builder.Property(e => e.AlisKdv)
                   .IsRequired(); // AlisKdv zorunlu

            builder.Property(e => e.SatisKdv)
                   .IsRequired(); // SatisKdv zorunlu

            // **İsteğe bağlı alanlar (nullable)**
            builder.Property(e => e.Barkod)
                   .HasMaxLength(50); // Barkod, isteğe bağlı, maksimum uzunluk 50 karakter

            builder.Property(e => e.BarkodTuru)
                   .HasMaxLength(20); // BarkodTuru, isteğe bağlı, maksimum uzunluk 20 karakter

            builder.Property(e => e.StokGrubu)
                   .HasMaxLength(50); // StokGrubu, isteğe bağlı, maksimum uzunluk 50 karakter

            builder.Property(e => e.StokAltGrubu)
                   .HasMaxLength(50); // StokAltGrubu, isteğe bağlı, maksimum uzunluk 50 karakter

            builder.Property(e => e.Marka)
                   .HasMaxLength(50); // Marka, isteğe bağlı, maksimum uzunluk 50 karakter

            builder.Property(e => e.Modeli)
                   .HasMaxLength(50); // Modeli, isteğe bağlı, maksimum uzunluk 50 karakter

            builder.Property(e => e.OzelKod1)
                   .HasMaxLength(30); // OzelKod1, isteğe bağlı, maksimum uzunluk 30 karakter

            builder.Property(e => e.OzelKod2)
                   .HasMaxLength(30); // OzelKod2, isteğe bağlı, maksimum uzunluk 30 karakter

            builder.Property(e => e.OzelKod3)
                   .HasMaxLength(30); // OzelKod3, isteğe bağlı, maksimum uzunluk 30 karakter

            builder.Property(e => e.OzelKod4)
                   .HasMaxLength(30); // OzelKod4, isteğe bağlı, maksimum uzunluk 30 karakter

            builder.Property(e => e.GarantiSuresi)
                   .HasMaxLength(20); // GarantiSuresi, isteğe bağlı, maksimum uzunluk 20 karakter

            builder.Property(e => e.UreticiKodu)
                   .HasMaxLength(50); // UreticiKodu, isteğe bağlı, maksimum uzunluk 50 karakter

            // **Stok Miktarları**
            builder.Property(e => e.StokMiktari)
                   .HasColumnType("decimal(18,3)")
                   .HasDefaultValue(0); // Genel stok miktarı

            builder.Property(e => e.WebStokMiktari)
                   .HasColumnType("decimal(18,3)")
                   .HasDefaultValue(0); // Web sitesi için stok miktarı

            builder.Property(e => e.TrendyolStokMiktari)
                   .HasColumnType("decimal(18,3)")
                   .HasDefaultValue(0); // Trendyol için stok miktarı

            builder.Property(e => e.HepsiburadaStokMiktari)
                   .HasColumnType("decimal(18,3)")
                   .HasDefaultValue(0); // Hepsiburada için stok miktarı

            // **Stok Takip Tipi**
            builder.Property(e => e.StokTakipTipi)
                   .HasDefaultValue(1); // Varsayılan: Bağımsız

            // **Platform bazlı fiyat yapısı**
            builder.Property(e => e.AlisFiyati)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0); // Ana alış fiyatı

            builder.Property(e => e.ToplamFiyati)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0); // Toptan satış fiyatı

            builder.Property(e => e.PerakendeFiyati)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0); // Perakende satış fiyatı

            builder.Property(e => e.WebFiyati)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0); // Web sitesi fiyatı

            builder.Property(e => e.TrendyolFiyati)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0); // Trendyol fiyatı

            builder.Property(e => e.HepsiburadaFiyati)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0); // Hepsiburada fiyatı

            // **Fiyat Hesaplama Tipi**
            builder.Property(e => e.FiyatHesaplamaTipi)
                   .HasDefaultValue(1); // Varsayılan: Bağımsız

            // **E-ticaret entegrasyonu için gerekli alanlar**
            builder.Property(e => e.TrendyolKodu)
                   .HasMaxLength(100); // Trendyol ürün kodu

            builder.Property(e => e.HepsiburadaKodu)
                   .HasMaxLength(100); // Hepsiburada ürün kodu

            builder.Property(e => e.WebKodu)
                   .HasMaxLength(100); // Web sitesi ürün kodu

            // **Platformlar için komisyon oranları**
            builder.Property(e => e.TrendyolKomisyon)
                   .HasColumnType("decimal(5,2)")
                   .HasDefaultValue(0); // Trendyol komisyon oranı (%)

            builder.Property(e => e.HepsiburadaKomisyon)
                   .HasColumnType("decimal(5,2)")
                   .HasDefaultValue(0); // Hepsiburada komisyon oranı (%)

            builder.Property(e => e.WebKomisyon)
                   .HasColumnType("decimal(5,2)")
                   .HasDefaultValue(0); // Web komisyon oranı (%)

            builder.Property(e => e.MinStokMiktari)
                   .HasColumnType("decimal(18,3)"); // MinStokMiktari, decimal(18,3) formatında

            builder.Property(e => e.MaxStokMiktari)
                   .HasColumnType("decimal(18,3)"); // MaxStokMiktari, decimal(18,3) formatında

            builder.Property(e => e.Aciklama)
                   .HasMaxLength(500); // Aciklama, isteğe bağlı, maksimum uzunluk 500 karakter
                   
            builder.Property(e => e.WebAciklama)
                   .HasMaxLength(500); // Web sitesi için açıklama
                   
            builder.Property(e => e.SonGuncelleme)
                   .HasColumnType("datetime2")
                   .HasDefaultValueSql("GETDATE()"); // Son güncelleme tarihi
                   
            builder.Property(e => e.GuncelleyenKullaniciId)
                   .HasDefaultValue(0); // Güncelleyen kullanıcı ID
        }
    }
}
