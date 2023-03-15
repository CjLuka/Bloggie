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

        public async Task<ServiceResponse<BlogPost>> UpdateAsync(Guid id)
        {
            //var Blog = _iBlogPostRepository.GetAsync(id);
            //var myBlog = await _iBlogPostRepository.GetAsync(id);
            
            //if (myBlog == null)
            //{
            //    return new ServiceResponse<BlogPost>()
            //    {
            //        Message = "Blog jest nullem",
            //        Success = false
            //    };
            //}
            var blogFromBase = await _iBlogPostRepository.GetAsync(id);
            if (blogFromBase == null)
            {
                return new ServiceResponse<BlogPost>()
                {
                    Message = "Nie znalezionio bloga o takim id",
                    Success = false
                };
            }
            
            await _iBlogPostRepository.UpdateAsync(blogFromBase);
            return new ServiceResponse<BlogPost>()
            {
                Data = blogFromBase
            };

            //await _iBlogPostRepository.UpdateAsync(blogFromBase);
            //return new ServiceResponse<BlogPost>()
            //{
            //    Data = blogPost,
            //    Message = "Edytowales blog",
            //    Success = true
            //};
        }
    }
}
