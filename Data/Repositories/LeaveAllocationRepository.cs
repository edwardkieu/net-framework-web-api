using Data.Infrastructure;
using Data.Interfaces;
using Domain.Entities;

namespace Data.Repositories
{
    public class LeaveAllocationRepository : RepositoryBase<LeaveAllocation>, ILeaveAllocationRepository
    {
        public LeaveAllocationRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}