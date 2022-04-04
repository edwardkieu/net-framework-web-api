using Data.Interfaces;
using Data.Repositories;
using System;
using System.Threading.Tasks;

namespace Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _dbContext;

        public IProductRepository ProductRepository => new ProductRepository(_dbContext);

        public IDepartmentRepository DepartmentRepository => new DepartmentRepository(_dbContext);

        public ILeaveAllocationRepository LeaveAllocationRepository => new LeaveAllocationRepository(_dbContext);

        public ILeaveTypeRepository LeaveTypeRepository => new LeaveTypeRepository(_dbContext);

        public IRequestLeaveRepository RequestLeaveRepository => new RequestLeaveRepository(_dbContext);

        public IRequestLeaveCommentRepository RequestLeaveCommentRepository => new RequestLeaveCommentRepository(_dbContext);

        public IAppUserRepository AppUserRepository => new AppUserRepository(_dbContext);

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UnitOfWork()
        {
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                }
            }
        }
    }
}