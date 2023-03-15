﻿using Domain.Models.Domain;
using Domain.Models.Response;
using Domain.Models.ViewModel.BlogPost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interface
{
    public interface IBlogPostServices
    {
        Task<ServiceResponse<IEnumerable<BlogPost>>> GetAllAsync();
        Task<ServiceResponse<BlogPost>> AddAsync(AddBlogPost post);
        Task<ServiceResponse<BlogPost>> UpdateAsync(Guid id);
        
    }
}
