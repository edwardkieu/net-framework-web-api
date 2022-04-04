

using Data.Interfaces;
using Domain.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly AppDbContext _dbContext;
        public AppUserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<AppUser> GetQuery()
        {
            return _dbContext.Users.AsQueryable();
        }

        public async Task<List<AppUser>> GetUserByDepartmentIdAsync(int departmentId)
        {
            var users = await _dbContext
                .Users
                .Where(x => x.Departments.Any(d => d.Id == departmentId))
                .AsNoTracking()
                .Include(x => x.Departments)
                .ToListAsync();

            return users;
        }
    }
}