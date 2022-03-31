using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}