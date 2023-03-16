using Domain.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using Persistance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggieDbContext _bloggieDbContext;
        public BlogPostRepository(BloggieDbContext bloggieDbContext)
        {
            _bloggieDbContext = bloggieDbContext;
        }

        public async Task AddAsync(BlogPost blogPost)
        {
            await _bloggieDbContext.AddAsync(blogPost);
            await _bloggieDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(BlogPost blogPost)
        {
            _bloggieDbContext.Remove(blogPost);
            await _bloggieDbContext.SaveChangesAsync();
        }

        //public bool Delete(Guid id)
        //{
        //    _bloggieDbContext.Remove(id);
        //    return true;
        //}

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            var blogPosts = await _bloggieDbContext.BlogPosts.ToListAsync();
            return blogPosts;
        }

        public async Task<BlogPost> GetAsync(Guid id)
        {
            var blogPost = await _bloggieDbContext.BlogPosts.FindAsync(id);
            return blogPost;
        }

        public async Task UpdateAsync(BlogPost blogPost)
        {
            _bloggieDbContext.Update(blogPost);
            await _bloggieDbContext.SaveChangesAsync();
        }
    }
}
