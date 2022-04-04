using AutoMapper;
using Domain.Entities;
using Service.ViewModels;
using System.Linq;

namespace Service.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductViewModel, Product>().ReverseMap();
            CreateMap<RequestLeave, RequestLeaveViewModel>().ForMember(des => des.Comment, source => source.MapFrom(f => f.Comments.Select(x => x.Comment).FirstOrDefault()));
            CreateMap<RequestLeaveViewModel, RequestLeave>();
        }
    }
}