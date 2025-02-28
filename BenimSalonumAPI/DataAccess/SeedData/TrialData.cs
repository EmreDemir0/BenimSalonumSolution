using System;
using System.Linq;
using System.Threading.Tasks;
using BenimSalonum.Entities.Tables;
using BenimSalonumAPI.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace BenimSalonum.DataAccess.SeedData
{
    public static class TrialData
    {
        public static async Task SeedAsync(BenimSalonumContext context)
        {
            if (!await context.Cariler.AnyAsync())
            {
                await context.Cariler.AddRangeAsync(
                new CariTable { Id = 1, CariTuru = "Musteri", CariGrubu = "M001", CariKodu = "C001", CariAdi = "Müşteri A", Durumu = true, YetkiliKisi = "Ali Veli", FaturaUnvani = "Ali Ticaret", CepTelefonu = "5551112233", Telefon = "02121234567", Il = "İstanbul", Ilce = "Kadıköy", Adres = "Örnek Mah. No:5" },
                new CariTable { Id = 2, CariTuru = "Musteri", CariGrubu = "M002", CariKodu = "C002", CariAdi = "Müşteri B", Durumu = true, YetkiliKisi = "Ayşe Demir", FaturaUnvani = "Ayşe Ltd. Şti.", CepTelefonu = "5552223344", Telefon = "03124455667", Il = "Ankara", Ilce = "Çankaya", Adres = "Bahçelievler Sok. No:10" }
                );
            }

            if (!await context.Stoklar.AnyAsync())
            {
                await context.Stoklar.AddRangeAsync(
                new StokTable { Id = 1, StokKodu = "S001", StokAdi = "Şampuan", Durumu = true, Barkod = "8691234567890", Birimi = "Adet", StokGrubu = "Kozmetik", Marka = "X Marka" },
                new StokTable { Id = 2, StokKodu = "S002", StokAdi = "Saç Kremi", Durumu = true, Barkod = "8690987654321", Birimi = "Adet", StokGrubu = "Kozmetik", Marka = "Y Marka" }
                );
            }

            if (!await context.Kasalar.AnyAsync())
            {
                await context.Kasalar.AddRangeAsync(
                new KasaTable { Id = 1, KasaKodu = "K001", KasaAdi = "Ana Kasa", YetkiliAdi = "Ali Veli", Aciklama = "Ana kasa" },
                new KasaTable { Id = 2, KasaKodu = "K002", KasaAdi = "Yedek Kasa", YetkiliAdi = "Ayşe Demir", Aciklama = "Yedek kasa" }
                );
            }

            if (!await context.Kullanicilar.AnyAsync())
            {
                await context.Kullanicilar.AddRangeAsync(
                new KullaniciTable { Id = 1, KullaniciAdi = "admin", Adi = "Admin", Soyadi = "User", Parola = "12345", Aktif = true, HatirlatmaSorusu = "Doğum Yılınız?", HatirlatmaCevap = "1990" },
                new KullaniciTable { Id = 2, KullaniciAdi = "kullanici1", Adi = "Ahmet", Soyadi = "Yılmaz", Parola = "12345", Aktif = true, HatirlatmaSorusu = "İlk Evcil Hayvanınızın Adı?", HatirlatmaCevap = "Boncuk" }
                );
            }

            if (!await context.Personeller.AnyAsync())
            {
                await context.Personeller.AddRangeAsync(
                new PersonelTable { Id = 1, PersonelAdi = "Zeynep", PersonelKodu = "P001", Durumu = true, PersonelUnvani = "Kasiyer", CepTelefonu = "5559998877", AylikMaas = 7500, PersonelTc = "11111111111", Adres = "ANKARA KEÇÖREN" },
                new PersonelTable { Id = 2, PersonelAdi = "Mehmet", PersonelKodu = "P002", Durumu = true, PersonelUnvani = "Depo Görevlisi", CepTelefonu = "5556665544", AylikMaas = 8000, PersonelTc = "11111111111", Adres = "ANKARA KEÇÖREN" }
                );
            }

            if (!await context.Tanimlar.AnyAsync())
            {
                await context.Tanimlar.AddRangeAsync(
                new TanimlarTable { Id = 1, Turu = "Ürün Grubu", Tanimi = "Kozmetik", Aciklama = "Kozmetik ürünleri kategorisi" },
                new TanimlarTable { Id = 2, Turu = "Hizmet", Tanimi = "Cilt Bakımı", Aciklama = "Cilt bakımı hizmetleri" }
                );
            }

            await context.SaveChangesAsync();
        }
    }
}
