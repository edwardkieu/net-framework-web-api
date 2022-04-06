using Data.Interfaces;
using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Infrastructure
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class, IEntityBase, new()
    {
        protected readonly AppDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        protected RepositoryBase(AppDbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }

        #region Implementation

        public virtual T Add(T entity)
        {
            return _dbSet.Add(entity);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void UpdateRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Update(entity);
            }
        }

        public virtual T Delete(T entity)
        {
            return _dbSet.Remove(entity);
        }

        public virtual T Delete(int id)
        {
            var entity = _dbSet.Find(id);
            return _dbSet.Remove(entity);
        }

        public virtual void DeleteRange(Expression<Func<T, bool>> predicate)
        {
            var objects = _dbSet.Where<T>(predicate).ToList();
            if (objects.Any())
                _dbSet.RemoveRange(objects);
        }

        public virtual void DeleteRange(IEnumerable<T> entities)
        {
            if (entities.Any())
                _dbSet.RemoveRange(entities);
        }

        public virtual async Task<T> FindByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<T> GetSingle(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<T> GetSingle(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<List<T>> FindByAsync(Expression<Func<T, bool>> predicate, bool isAsNoTracking = false)
        {
            var query = _dbSet.Where(predicate);
            if (isAsNoTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }

        public virtual async Task<List<T>> FindByAsync(Expression<Func<T, bool>> predicate, bool isAsNoTracking = false, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (isAsNoTracking)
                query = query.AsNoTracking();

            return await query.Where(predicate).ToListAsync();
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.CountAsync(predicate);
        }

        public IQueryable<T> GetAllQuery(bool isAsNoTracking = false, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (isAsNoTracking)
            {
                query = query.AsNoTracking();
            }

            return query;
        }

        #endregion Implementation
    }
}
