using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Persistance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class ImageRepositoryCloudinary : IImageRepository
    {
        private readonly Account _account;
        public ImageRepositoryCloudinary(IConfiguration configuration)
        {
            _account = new Account(configuration.GetSection("Cloudinary")["CloudName"],
                configuration.GetSection("Cloudinary")["ApiKey"],
                configuration.GetSection("Cloudinary")["ApiSecret"]);
        }
        public async Task<string> UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(_account);
            var uploadFileResult = await client.UploadAsync(
                new CloudinaryDotNet.Actions.ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                    DisplayName = file.Name,
                });
            if (uploadFileResult != null && uploadFileResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadFileResult.SecureUrl.ToString();
            }
            return null;
        }
    }
}
