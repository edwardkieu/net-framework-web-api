using AutoMapper;
using Data.Interfaces;
using Domain.Entities;
using Service.Interfaces;
using Service.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class ProductService : IProductService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductViewModel> AddAsync(ProductViewModel vm)
        {
            var product = _mapper.Map<Product>(vm);
            _unitOfWork.ProductRepository.Add(product);
             await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ProductViewModel>(product);
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<ProductViewModel>>(products);
        }
    }
}