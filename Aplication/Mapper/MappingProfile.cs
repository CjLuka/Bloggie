using AutoMapper;
using Domain.Models.Domain;
using Domain.Models.ViewModel.BlogPost;

namespace Aplication.Mapper
{
    public  class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<BlogPost, AddBlogPost>().ReverseMap();
            CreateMap<AddBlogPost, BlogPost>()
                .ForMember(d => d.id, o => o.Ignore())
                .ReverseMap();
        }
    }
}
