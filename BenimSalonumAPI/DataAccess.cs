using BenimSalonumAPI.DataAccess.Context;
using BenimSalonum.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BenimSalonumAPI
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
            var entity = await _dbSet.FindAsync(id) ?? throw new KeyNotFoundException($"Entity with id {id} not found.");
            return entity;
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync(); // Burada zaten asenkron bir işlem yapılmaktadır.
        }

        public async Task RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(); // Burada da bir asenkron işlem yapılmaktadır.
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync(); // Burada da bir asenkron işlem yapılmaktadır.
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        // Bu metodu sadece veritabanı işlemleri dışarıda tetiklendiğinde çağırın
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}




