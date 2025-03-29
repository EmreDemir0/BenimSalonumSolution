using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenimSalonum.Entities.Tables;
using BenimSalonumAPI.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace BenimSalonumAPI.DataAccess.Repositories
{
    public class FaturaRepository : GenericRepository<FaturaTable>
    {
        public FaturaRepository(BenimSalonumContext context) : base(context)
        {
        }

        // Fatura ve ilişkili detayları birlikte getiren metod
        public async Task<FaturaTable> GetFaturaWithDetailsAsync(int faturaId)
        {
            return await _context.Faturalar
                .Include(f => f.FaturaDetaylari)
                .Include(f => f.Cari)
                .Include(f => f.Sube)
                .FirstOrDefaultAsync(f => f.Id == faturaId);
        }

        // Belirli bir cari için faturaları getiren metod
        public async Task<IEnumerable<FaturaTable>> GetFaturalarByCariAsync(int cariId)
        {
            return await _context.Faturalar
                .Where(f => f.CariId == cariId && f.FaturaDurumu != 4) // 4: İptal Edildi
                .Include(f => f.FaturaDetaylari)
                .OrderByDescending(f => f.FaturaTarihi)
                .ToListAsync();
        }

        // Tarih aralığına göre faturaları getiren metod
        public async Task<IEnumerable<FaturaTable>> GetFaturalarByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Faturalar
                .Where(f => f.FaturaTarihi >= startDate && f.FaturaTarihi <= endDate && f.FaturaDurumu != 4) // 4: İptal Edildi
                .Include(f => f.Cari)
                .OrderByDescending(f => f.FaturaTarihi)
                .ToListAsync();
        }

        // Fatura türüne göre faturaları getiren metod
        public async Task<IEnumerable<FaturaTable>> GetFaturalarByTypeAsync(int faturaTuru)
        {
            return await _context.Faturalar
                .Where(f => f.FaturaTuru == faturaTuru && f.FaturaDurumu != 4) // 4: İptal Edildi
                .Include(f => f.Cari)
                .OrderByDescending(f => f.FaturaTarihi)
                .ToListAsync();
        }

        // E-Fatura durumuna göre faturaları getiren metod
        public async Task<IEnumerable<FaturaTable>> GetFaturalarByEFaturaDurumuAsync(string efaturaDurum)
        {
            return await _context.Faturalar
                .Where(f => f.EfaturaDurum == efaturaDurum && f.FaturaDurumu != 4) // 4: İptal Edildi
                .Include(f => f.Cari)
                .OrderByDescending(f => f.FaturaTarihi)
                .ToListAsync();
        }

        // Yeni fatura numarası oluşturan metod
        public async Task<string> CreateNewFaturaNoAsync(int faturaTuru, int subeId)
        {
            var year = DateTime.Now.Year.ToString();
            string prefix;
            
            // Fatura türüne göre prefix oluştur
            switch(faturaTuru)
            {
                case 1: // Satış
                    prefix = "S";
                    break;
                case 2: // Alış
                    prefix = "A";
                    break;
                case 3: // İade
                    prefix = "I";
                    break;
                case 4: // Masraf
                    prefix = "M";
                    break;
                default:
                    prefix = "F";
                    break;
            }
            
            prefix += subeId.ToString("D2"); // F01, S01 gibi prefix oluştur
            
            var lastFatura = await _context.Faturalar
                .Where(f => f.FaturaTuru == faturaTuru && f.SubeId == subeId && f.FaturaNo.StartsWith(prefix + year))
                .OrderByDescending(f => f.FaturaNo)
                .FirstOrDefaultAsync();

            int nextNumber = 1;
            if (lastFatura != null)
            {
                var lastNumberPart = lastFatura.FaturaNo.Substring((prefix + year).Length);
                if (int.TryParse(lastNumberPart, out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            return $"{prefix}{year}{nextNumber.ToString("D6")}";
        }
    }
}
