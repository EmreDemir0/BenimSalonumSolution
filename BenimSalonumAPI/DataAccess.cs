using BenimSalonumAPI.DataAccess.Context;
using BenimSalonum.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BenimSalonumAPI.DataAccess
{
    public class DataAccess<T> : IRepository<T> where T : class
    {
        private readonly BenimSalonumContext _context;
        private readonly DbSet<T> _dbSet;

        public DataAccess(BenimSalonumContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task UpdateAsync(T entity) // ✅ Update metodunu ekledik
        {
            _dbSet.Update(entity);
        }

        public async Task RemoveAsync(T entity) // ✅ Remove metodunu ekledik
        {
            _dbSet.Remove(entity);
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities) // ✅ RemoveRange ekledik
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<int> SaveChangesAsync() // ✅ SaveChangesAsync ekledik
        {
            return await _context.SaveChangesAsync();
        }
    }
}
