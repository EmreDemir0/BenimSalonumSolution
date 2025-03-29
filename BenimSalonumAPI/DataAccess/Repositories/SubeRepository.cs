using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenimSalonum.Entities.Tables;
using BenimSalonumAPI.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace BenimSalonumAPI.DataAccess.Repositories
{
    public class SubeRepository : GenericRepository<SubeTable>
    {
        public SubeRepository(BenimSalonumContext context) : base(context)
        {
        }

        // Aktif şubeleri getiren metod
        public async Task<IEnumerable<SubeTable>> GetActiveSubelerAsync()
        {
            return await _context.Subeler
                .Where(s => s.AktifMi)
                .OrderBy(s => s.SubeAdi)
                .ToListAsync();
        }

        // Şube koduna göre şube getiren metod
        public async Task<SubeTable> GetSubeByKodAsync(string subeKodu)
        {
            return await _context.Subeler
                .FirstOrDefaultAsync(s => s.SubeKodu == subeKodu);
        }

        // Lisansı geçerli olan şubeleri getiren metod
        public async Task<IEnumerable<SubeTable>> GetSubelerWithValidLicenseAsync()
        {
            var today = DateTime.Today;
            return await _context.Subeler
                .Where(s => s.AktifMi && s.LisansBitisTarihi.HasValue && s.LisansBitisTarihi.Value >= today)
                .OrderBy(s => s.SubeAdi)
                .ToListAsync();
        }

        // Lisansı yakında bitecek şubeleri getiren metod
        public async Task<IEnumerable<SubeTable>> GetSubelerWithExpiringLicenseAsync(int gunSayisi = 30)
        {
            var today = DateTime.Today;
            var expiryDate = today.AddDays(gunSayisi);
            
            return await _context.Subeler
                .Where(s => s.AktifMi && 
                       s.LisansBitisTarihi.HasValue && 
                       s.LisansBitisTarihi.Value >= today && 
                       s.LisansBitisTarihi.Value <= expiryDate)
                .OrderBy(s => s.LisansBitisTarihi)
                .ToListAsync();
        }

        // Yeni şube kodu oluşturan metod
        public async Task<string> CreateNewSubeKoduAsync()
        {
            var prefix = "SB";
            
            var lastSube = await _context.Subeler
                .Where(s => s.SubeKodu.StartsWith(prefix))
                .OrderByDescending(s => s.SubeKodu)
                .FirstOrDefaultAsync();

            int nextNumber = 1;
            if (lastSube != null)
            {
                var lastNumberPart = lastSube.SubeKodu.Substring(prefix.Length);
                if (int.TryParse(lastNumberPart, out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            return $"{prefix}{nextNumber.ToString("D3")}";
        }
    }
}
