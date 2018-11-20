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
        public Task Create(T item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(T item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<T> Get(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<T> Get(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExist(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task Update(T item)
        {
            throw new NotImplementedException();
        }
    }
}
