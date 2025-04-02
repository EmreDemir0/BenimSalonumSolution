using System;
using System.Threading.Tasks;
using BenimSalonum.Entities.Tables;

namespace BenimSalonumAPI.Services.Interfaces
{
    public interface ILisansService
    {
        /// <summary>
        /// Online lisans kontrolü yapar
        /// </summary>
        Task<bool> LisansKontrolEtAsync(string lisansKodu, int firmaId);
        
        /// <summary>
        /// Offline lisans kontrolü yapar (yerel şifreli veriyi kullanır)
        /// </summary>
        Task<bool> OfflineLisansKontrolEtAsync(int firmaId);
        
        /// <summary>
        /// Yeni lisans aktivasyonu yapar
        /// </summary>
        Task<LisansTable> LisansAktifleştirAsync(string lisansKodu, string firmaAdi, int firmaId);
        
        /// <summary>
        /// Günlük lisans kontrolü yapar (uygulama açılışında çalışır)
        /// </summary>
        Task<bool> GunlukLisansKontrolEtAsync(int firmaId);
        
        /// <summary>
        /// Lisansın geçerli olup olmadığını kontrol eder
        /// </summary>
        Task<bool> LisansGecerliMiAsync(int firmaId);
        
        /// <summary>
        /// Lisans hakkında bilgi verir
        /// </summary>
        Task<LisansTable> LisansBilgisiGetirAsync(int firmaId);
    }
}
