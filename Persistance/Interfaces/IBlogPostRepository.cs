using Domain.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Interfaces
{
    public interface IBlogPostRepository
    {
        Task AddAsync(BlogPost blogPost);
        Task UpdateAsync(BlogPost blogPost);
        Task DeleteAsync(BlogPost blogPost);
        //bool Delete(Guid id);
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task <BlogPost> GetAsync(Guid id);
    }
}
