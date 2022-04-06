using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IEntityService<T> where T : IEntityBase
    {
        Task AddAsync(T entity);

        Task DeleteAsync(T entity);

        Task DeleteRangeAsync(IEnumerable<T> entities);

        Task<List<T>> FindByAsync(Expression<Func<T, bool>> predicate, bool isAsNoTracking = false, params Expression<Func<T, object>>[] includeProperties);

        Task UpdateAsync(T entity);

        Task UpdateRangeAsync(IEnumerable<T> entities);
    }
}