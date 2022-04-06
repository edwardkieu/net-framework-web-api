using Data;
using Data.Infrastructure;
using Data.Interfaces;
using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public abstract class EntityService<T> where T : class, IEntityBase, new()
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<T> _repository;

        public EntityService(IUnitOfWork unitOfWork, IRepository<T> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task AddAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity is null.");

            _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity is null.");

            _repository.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            if (entities.Any())
            {
                _repository.DeleteRange(entities);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<List<T>> FindByAsync(Expression<Func<T, bool>> predicate, bool isAsNoTracking = false, params Expression<Func<T, object>>[] includeProperties)
        {
            return await FindByAsync(predicate, isAsNoTracking, includeProperties);
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity is null.");

            _repository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            if (entities.Any())
            {
                _repository.UpdateRange(entities);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}