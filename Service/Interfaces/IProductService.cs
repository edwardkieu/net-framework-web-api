using Service.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IProductService
    {
        Task<ProductViewModel> AddAsync(ProductViewModel vm);

        Task<IEnumerable<ProductViewModel>> GetAllAsync();
    }
}