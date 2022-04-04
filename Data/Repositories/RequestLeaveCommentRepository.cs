using Data.Infrastructure;
using Data.Interfaces;
using Domain.Entities;

namespace Data.Repositories
{
    public class RequestLeaveCommentRepository : RepositoryBase<RequestLeaveComment>, IRequestLeaveCommentRepository
    {
        public RequestLeaveCommentRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}