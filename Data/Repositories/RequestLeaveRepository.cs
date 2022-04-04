using Data.Infrastructure;
using Data.Interfaces;
using Domain.Entities;

namespace Data.Repositories
{
    public class RequestLeaveRepository : RepositoryBase<RequestLeave>, IRequestLeaveRepository
    {
        public RequestLeaveRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}