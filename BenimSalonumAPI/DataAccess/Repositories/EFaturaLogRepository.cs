using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenimSalonum.Entities.Tables;
using BenimSalonumAPI.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace BenimSalonumAPI.DataAccess.Repositories
{
    public class EFaturaLogRepository : GenericRepository<EFaturaLogTable>
    {
        public EFaturaLogRepository(BenimSalonumContext context) : base(context)
        {
        }

        // Belirli bir faturanın tüm loglarını getiren metod
        public async Task<IEnumerable<EFaturaLogTable>> GetLogsByFaturaIdAsync(int faturaId)
        {
            return await _context.EFaturaLoglari
                .Where(l => l.FaturaId == faturaId)
                .OrderByDescending(l => l.IslemTarihi)
                .ToListAsync();
        }

        // Belirli bir tarih aralığındaki logları getiren metod
        public async Task<IEnumerable<EFaturaLogTable>> GetLogsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.EFaturaLoglari
                .Where(l => l.IslemTarihi >= startDate && l.IslemTarihi <= endDate)
                .OrderByDescending(l => l.IslemTarihi)
                .ToListAsync();
        }

        // Belirli bir işlem türüne göre logları getiren metod
        public async Task<IEnumerable<EFaturaLogTable>> GetLogsByIslemTuruAsync(int islemTuru)
        {
            return await _context.EFaturaLoglari
                .Where(l => l.IslemTuru == islemTuru)
                .OrderByDescending(l => l.IslemTarihi)
                .ToListAsync();
        }

        // Başarısız işlemlerin loglarını getiren metod
        public async Task<IEnumerable<EFaturaLogTable>> GetFailedLogsAsync()
        {
            return await _context.EFaturaLoglari
                .Where(l => l.IslemDurumu == 3 || !string.IsNullOrEmpty(l.HataMesaji)) // 3: Hata
                .OrderByDescending(l => l.IslemTarihi)
                .ToListAsync();
        }

        // Son n adet logu getiren metod
        public async Task<IEnumerable<EFaturaLogTable>> GetLastLogsAsync(int count)
        {
            return await _context.EFaturaLoglari
                .OrderByDescending(l => l.IslemTarihi)
                .Take(count)
                .ToListAsync();
        }
    }
}
