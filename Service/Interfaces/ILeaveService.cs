using Common.Enums;
using Service.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ILeaveService
    {
        Task<bool> AllLocateLeaveByDepartment(int departmentId, int period);

        Task<RequestLeaveViewModel> CreateRequestLeaveAsync(CreateRequestLeaveViewModel vm);

        Task<bool> ChangeStatusAsync(int id, RequestLeaveStatus status);

        Task<RequestLeaveViewModel> GetByIdAsync(int id);

        Task<IEnumerable<RequestLeaveViewModel>> GetByDepartmentIdAsync(int departmentId);
    }
}