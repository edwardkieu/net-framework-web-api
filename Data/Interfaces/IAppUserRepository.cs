using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IAppUserRepository
    {
        IQueryable<AppUser> GetQuery();
        Task<List<AppUser>> GetUserByDepartmentIdAsync(int departmentId);
    }
}