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

        //Usuwanie postu
        public async Task<ServiceResponse<BlogPost>> DeleteAsync(Guid id)
        {
            var blog = await _iBlogPostRepository.GetByIdAsync(id);
            if (blog == null)
            {
                return new ServiceResponse<BlogPost>()
                {
                    Message = "Blog nie istnieje",
                    Success = false
                };
            }
            await _iBlogPostRepository.DeleteAsync(blog);
            return new ServiceResponse<BlogPost>()
            {
                Message = "Usunieto post",
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

        //Pobranie danych na temat postu
        public async Task<ServiceResponse<BlogPost>> GetDetailBlogPost(Guid id)
        {
            var blog = await _iBlogPostRepository.GetByIdAsync(id);
            return new ServiceResponse<BlogPost>()
            {
                Data= blog,
                Message = "Twoj blog",
                Success = true
            };
        }

        public async Task<ServiceResponse<BlogPost>> GetDetailBlogPost(string urlHandle)
        {
            var blogPost = await _iBlogPostRepository.GetByUrlAsync(urlHandle);
            return new ServiceResponse<BlogPost>()
            {
                Data = blogPost,
                Message = "Twoj blog",
                Success = true
            };
        }

        //Edytowanie postu
        public async Task<ServiceResponse<BlogPost>> UpdateAsync(BlogPost blogPost, Guid id)
        {

            var blogFromBase = await _iBlogPostRepository.GetByIdAsync(blogPost.id);//pobranie postu z bazy po Id
            if (blogFromBase == null)
            {
                return new ServiceResponse<BlogPost>()
                {
                    Message = "Nie znalezionio bloga o takim id",
                    Success = false
                };
            }
            
            blogFromBase.Heading = blogPost.Heading;
            blogFromBase.PublishedDate = blogPost.PublishedDate;
            blogFromBase.PageTitle = blogPost.PageTitle;
            blogFromBase.Author = blogPost.Author;
            blogFromBase.Content = blogPost.Content;
            blogFromBase.FeaturedImageUrl = blogPost.FeaturedImageUrl;
            blogFromBase.Visible = blogPost.Visible;
            blogFromBase.UrlHandle = blogPost.UrlHandle;
            //blogFromBase.ShortDescription = blogPost.ShortDescription;

            
            await _iBlogPostRepository.UpdateAsync(blogFromBase);
            return new ServiceResponse<BlogPost>()
            {
                Data = blogFromBase
            };
        }
    }
}
