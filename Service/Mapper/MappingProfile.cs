using AutoMapper;
using Domain.Entities;
using Service.ViewModels;

namespace Service.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductViewModel, Product>().ReverseMap();
        }

        //public static void Configure()
        //{
        //    AutoMapper.Mapper.Initialize(config =>
        //    {
        //        config.CreateMap<Product, ProductViewModel>().ReverseMap();
        //    });
        //}
    }
}