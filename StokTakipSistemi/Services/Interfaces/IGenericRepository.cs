using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StokTakipSistemi.Services.Interfaces
{
    public interface IGenericRepository<T> where T: class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate);
        Task<T> GetAsync(int? id);
        Task<T> Get(Expression<Func<T, bool>> predicate);
        Task CreateAsync(T item);
        Task Update(T item);
        Task DeleteAsync(T item);
        Task DeleteAsync(int? id);
        Task<bool> IsExist(Expression<Func<T, bool>> expression);
    }
}
