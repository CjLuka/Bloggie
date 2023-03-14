using Aplication.Interface;
using AutoMapper;
using Domain.Models.Domain;
using Domain.Models.Response;
using Domain.Models.ViewModel.BlogPost;
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

        //dodaj nowy blogpost
        public async Task<ServiceResponse<BlogPost>> AddAsync(AddBlogPost post)
        {
            //var newBlog = _iBlogPostRepository.AddAsync(_mapper.Map<BlogPost>(post));
            //if (newBlog == null)
            //{
            //    return new ServiceResponse<BlogPost>()
            //    {
            //        Message = "Nie znaleziono zadnego posta",
            //        Success = false
            //    };
            //}
            //return new ServiceResponse<BlogPost>()
            //{
            //    Data = _mapper.Map<BlogPost>(newBlog),
            //    Message = "Wszystkie posty",
            //    Success = true
            //};
            var mappedBlogPost = _mapper.Map<BlogPost>(post);
            await _iBlogPostRepository.AddAsync(mappedBlogPost);

            if (mappedBlogPost.id == Guid.Empty)
            {
                return new ServiceResponse<BlogPost>()
                {
                    Message = "Nie udalo sie zapisac tego rekordu do bazy danych",
                    Success = false
                };
            }
            return new ServiceResponse<BlogPost>()
            {
                Data = mappedBlogPost,
                Message = "Dodano post",
                Success = true
            };
        }

        //asynchroniczne pobranie wszystkich postów
        public async Task<ServiceResponse<IEnumerable<BlogPost>>> GetAllAsync()
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
