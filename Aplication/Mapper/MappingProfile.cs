using AutoMapper;
using Domain.Models.Domain;
using Domain.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<BlogPost, AddBlogPost>().ReverseMap();
        }
    }
}
