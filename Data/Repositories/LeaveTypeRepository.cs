using Data.Infrastructure;
using Data.Interfaces;
using Domain.Entities;

namespace Data.Repositories
{
    public class LeaveTypeRepository : RepositoryBase<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}