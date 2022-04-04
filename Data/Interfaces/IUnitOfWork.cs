using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        ILeaveAllocationRepository LeaveAllocationRepository { get; }
        ILeaveTypeRepository LeaveTypeRepository { get; }
        IRequestLeaveRepository RequestLeaveRepository { get; }
        IRequestLeaveCommentRepository RequestLeaveCommentRepository { get; }
        IAppUserRepository AppUserRepository { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}