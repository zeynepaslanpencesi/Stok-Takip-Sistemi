using Microsoft.EntityFrameworkCore;
using StokTakipSistemi.Data;
using StokTakipSistemi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StokTakipSistemi.Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly StokTakipSistemiDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(StokTakipSistemiDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public async Task Create(T item)
        {
            await _dbSet.AddAsync(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(T item)
        {
            _dbSet.Remove(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            var entity = await Get(id);
            
        }

        public async Task<T> Get(int? id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> Get(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).FirstOrDefaultAsync();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsEnumerable();
        }

        public Task<bool> IsExist(Expression<Func<T, bool>> expression)
        {
            return _dbSet.AsNoTracking().AnyAsync(expression);
        }

        public async Task Update(T item)
        {
            _dbSet.Attach(item);
            _dbContext.Entry(item).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
