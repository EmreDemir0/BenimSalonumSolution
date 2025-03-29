using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenimSalonum.Entities.Tables;
using BenimSalonumAPI.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace BenimSalonumAPI.DataAccess.Repositories
{
    public class SiparisRepository : GenericRepository<SiparisTable>
    {
        public SiparisRepository(BenimSalonumContext context) : base(context)
        {
        }

        // Sipariş ve ilişkili detayları birlikte getiren metod
        public async Task<SiparisTable> GetSiparisWithDetailsAsync(int siparisId)
        {
            return await _context.Siparisler
                .Include(s => s.SiparisDetaylari)
                .Include(s => s.Cari)
                .Include(s => s.Sube)
                .FirstOrDefaultAsync(s => s.Id == siparisId);
        }

        // Belirli bir cari için siparişleri getiren metod
        public async Task<IEnumerable<SiparisTable>> GetSiparislerByCariAsync(int cariId)
        {
            return await _context.Siparisler
                .Where(s => s.CariId == cariId && s.SiparisDurumu != 5) // 5: İptal
                .Include(s => s.SiparisDetaylari)
                .OrderByDescending(s => s.SiparisTarihi)
                .ToListAsync();
        }

        // Tarih aralığına göre siparişleri getiren metod
        public async Task<IEnumerable<SiparisTable>> GetSiparislerByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Siparisler
                .Where(s => s.SiparisTarihi >= startDate && s.SiparisTarihi <= endDate && s.SiparisDurumu != 5) // 5: İptal
                .Include(s => s.Cari)
                .OrderByDescending(s => s.SiparisTarihi)
                .ToListAsync();
        }

        // Sipariş türüne göre siparişleri getiren metod
        public async Task<IEnumerable<SiparisTable>> GetSiparislerByTypeAsync(int siparisTuru)
        {
            return await _context.Siparisler
                .Where(s => s.SiparisTuru == siparisTuru && s.SiparisDurumu != 5) // 5: İptal
                .Include(s => s.Cari)
                .OrderByDescending(s => s.SiparisTarihi)
                .ToListAsync();
        }

        // Sipariş durumuna göre siparişleri getiren metod
        public async Task<IEnumerable<SiparisTable>> GetSiparislerByDurumAsync(int siparisDurumu)
        {
            return await _context.Siparisler
                .Where(s => s.SiparisDurumu == siparisDurumu && s.SiparisDurumu != 5) // 5: İptal
                .Include(s => s.Cari)
                .OrderByDescending(s => s.SiparisTarihi)
                .ToListAsync();
        }

        // Yeni sipariş numarası oluşturan metod
        public async Task<string> CreateNewSiparisNoAsync(int siparisTuru, int subeId)
        {
            var year = DateTime.Now.Year.ToString();
            string prefix;
            
            // Sipariş türüne göre prefix oluştur
            switch(siparisTuru)
            {
                case 1: // Satış
                    prefix = "S";
                    break;
                case 2: // Alış
                    prefix = "A";
                    break;
                default:
                    prefix = "S";
                    break;
            }
            
            prefix += subeId.ToString("D2"); // S01, A01 gibi prefix oluştur
            
            var lastSiparis = await _context.Siparisler
                .Where(s => s.SiparisTuru == siparisTuru && s.SubeId == subeId && s.SiparisNo.StartsWith(prefix + year))
                .OrderByDescending(s => s.SiparisNo)
                .FirstOrDefaultAsync();

            int nextNumber = 1;
            if (lastSiparis != null)
            {
                var lastNumberPart = lastSiparis.SiparisNo.Substring((prefix + year).Length);
                if (int.TryParse(lastNumberPart, out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            return $"{prefix}{year}{nextNumber.ToString("D6")}";
        }

        // Siparişleri faturaya dönüştürme durumunu kontrol eden metod
        public async Task<IEnumerable<SiparisTable>> GetSiparislerForFaturaAsync()
        {
            return await _context.Siparisler
                .Where(s => s.SiparisDurumu == 4 && !s.FaturaKesildi && s.SiparisDurumu != 5) // 4: Tamamlandı, 5: İptal
                .Include(s => s.SiparisDetaylari)
                .Include(s => s.Cari)
                .OrderByDescending(s => s.SiparisTarihi)
                .ToListAsync();
        }
    }
}
