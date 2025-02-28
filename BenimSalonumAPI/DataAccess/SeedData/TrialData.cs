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
                new CariTable {CariTuru = "Musteri", CariGrubu = "M001", CariKodu = "C001", CariAdi = "Müşteri A", Durumu = true, YetkiliKisi = "Ali Veli", FaturaUnvani = "Ali Ticaret", CepTelefonu = "5551112233", Telefon = "02121234567", Il = "İstanbul", Ilce = "Kadıköy", Adres = "Örnek Mah. No:5" },
                new CariTable {CariTuru = "Musteri", CariGrubu = "M002", CariKodu = "C002", CariAdi = "Müşteri B", Durumu = true, YetkiliKisi = "Ayşe Demir", FaturaUnvani = "Ayşe Ltd. Şti.", CepTelefonu = "5552223344", Telefon = "03124455667", Il = "Ankara", Ilce = "Çankaya", Adres = "Bahçelievler Sok. No:10" }
                );
            }

            if (!await context.Stoklar.AnyAsync())
            {
                await context.Stoklar.AddRangeAsync(
                new StokTable  {StokKodu = "S001", StokAdi = "Şampuan", Durumu = true, Barkod = "8691234567890", Birimi = "Adet", StokGrubu = "Kozmetik", Marka = "X Marka" },
                new StokTable {StokKodu = "S002", StokAdi = "Saç Kremi", Durumu = true, Barkod = "8690987654321", Birimi = "Adet", StokGrubu = "Kozmetik", Marka = "Y Marka" }
                );
            }

            if (!await context.Kasalar.AnyAsync())
            {
                await context.Kasalar.AddRangeAsync(
                new KasaTable {KasaKodu = "K001", KasaAdi = "Ana Kasa", YetkiliAdi = "Ali Veli", Aciklama = "Ana kasa" },
                new KasaTable {KasaKodu = "K002", KasaAdi = "Yedek Kasa", YetkiliAdi = "Ayşe Demir", Aciklama = "Yedek kasa" }
                );
            }

            if (!await context.Kullanicilar.AnyAsync())
            {
                await context.Kullanicilar.AddRangeAsync(
                new KullaniciTable {KullaniciAdi = "admin", Adi = "Admin", Soyadi = "User", Parola = "12345", Aktif = true, HatirlatmaSorusu = "Doğum Yılınız?", HatirlatmaCevap = "1990" },
                new KullaniciTable {KullaniciAdi = "kullanici1", Adi = "Ahmet", Soyadi = "Yılmaz", Parola = "12345", Aktif = true, HatirlatmaSorusu = "İlk Evcil Hayvanınızın Adı?", HatirlatmaCevap = "Boncuk" }
                );
            }

            if (!await context.Personeller.AnyAsync())
            {
                await context.Personeller.AddRangeAsync(
                new PersonelTable {PersonelAdi = "Zeynep", PersonelKodu = "P001", Durumu = true, PersonelUnvani = "Kasiyer", CepTelefonu = "5559998877", AylikMaas = 7500, PersonelTc = "11111111111", Adres = "ANKARA KEÇÖREN" },
                new PersonelTable {PersonelAdi = "Mehmet", PersonelKodu = "P002", Durumu = true, PersonelUnvani = "Depo Görevlisi", CepTelefonu = "5556665544", AylikMaas = 8000, PersonelTc = "11111111111", Adres = "ANKARA KEÇÖREN" }
                );
            }

            if (!await context.Tanimlar.AnyAsync())
            {
                await context.Tanimlar.AddRangeAsync(
                new TanimlarTable {Turu = "Ürün Grubu", Tanimi = "Kozmetik", Aciklama = "Kozmetik ürünleri kategorisi" },
                new TanimlarTable {Turu = "Hizmet", Tanimi = "Cilt Bakımı", Aciklama = "Cilt bakımı hizmetleri" }
                );
            }

            await context.SaveChangesAsync();
        }
    }
}
