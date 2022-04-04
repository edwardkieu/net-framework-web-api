using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        // Marks an entity as new
        T Add(T entity);

        void AddRange(IEnumerable<T> entities);

        // Marks an entity as modified
        void Update(T entity);

        void UpdateRange(IEnumerable<T> entities);

        // Marks an entity to be removed
        T Delete(T entity);

        T Delete(int id);

        //Delete multi records
        void DeleteRange(Expression<Func<T, bool>> predicate);

        // Get an entity by int id
        Task<T> FindByIdAsync(object id);

        Task<T> GetSingle(int id);

        Task<T> GetSingle(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetAllQuery(bool isAsNoTracking = false, params Expression<Func<T, object>>[] includeProperties);

        Task<List<T>> FindByAsync(Expression<Func<T, bool>> predicate, bool isAsNoTracking = false);

        Task<List<T>> FindByAsync(Expression<Func<T, bool>> predicate, bool isAsNoTracking = false, params Expression<Func<T, object>>[] includeProperties);

        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    }
}