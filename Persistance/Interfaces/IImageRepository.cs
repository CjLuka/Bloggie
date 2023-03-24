using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Interfaces
{
    public interface IImageRepository
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
