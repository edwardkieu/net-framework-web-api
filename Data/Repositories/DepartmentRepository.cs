using Data.Infrastructure;
using Data.Interfaces;
using Domain.Entities;

namespace Data.Repositories
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}