using Aplication.Interface;
using AutoMapper;
using Domain.Models.Domain;
using Domain.Models.Response;
using Domain.Models.ViewModel;
using Persistance.Interfaces;
using Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public class BlogPostServices : IBlogPostServices
    {
        private readonly IBlogPostRepository _iBlogPostRepository;
        private readonly IMapper _mapper;
        public BlogPostServices(IBlogPostRepository iblogPostRepository, IMapper mapper)
        {
            _iBlogPostRepository = iblogPostRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<BlogPost>> AddAsync(AddBlogPost post)
        {
            var newBlog = _iBlogPostRepository.AddAsync(_mapper.Map<BlogPost>(post));
            if (newBlog == null)
            {
                return new ServiceResponse<BlogPost>()
                {
                    Message = "Nie znaleziono zadnego posta",
                    Success = false
                };
            }
            return new ServiceResponse<BlogPost>()
            {
                Data = _mapper.Map<BlogPost>(newBlog),
                Message = "Wszystkie posty",
                Success = true
            };
        }


        public async Task<ServiceResponse<IEnumerable<BlogPost>>> GetAllAsync()//asynchroniczne pobranie wszystkich postów
        {
            var AllBlogs = await _iBlogPostRepository.GetAllAsync();
            if(AllBlogs == null)
            {
                return new ServiceResponse<IEnumerable<BlogPost>>()
                {
                    Message = "Nie znaleziono zadnego posta",
                    Success = false
                };
            }
            return new ServiceResponse<IEnumerable<BlogPost>>()
            {
                Data  = AllBlogs,
                Message = "Wszystkie posty",
                Success = true
            };
        }
    }
}
