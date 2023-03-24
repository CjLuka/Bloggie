using Aplication.Interface;
using Domain.Models.ViewModel;
using Domain.Models.ViewModel.BlogPost;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistance.Data;
using System.Text.Json;

namespace BloggieWeb.Pages.Admin.Blogs
{
    public class AddModel : PageModel
    {
        private readonly IBlogPostServices _blogPostServices;
        public AddModel(IBlogPostServices blogPostServices)
        {
            _blogPostServices = blogPostServices;
        }

        [BindProperty]
        public AddBlogPost AddBlogPostRequest { get; set; }
        [BindProperty]
        public IFormFile FeaturedImage { get; set; }
        public void OnGet()
        {
            
        }
        public async Task<IActionResult> OnPost()
        {
            
            if (AddBlogPostRequest.Heading.IsNullOrEmpty()|| AddBlogPostRequest.Author.IsNullOrEmpty() || 
                AddBlogPostRequest.Content.IsNullOrEmpty() || AddBlogPostRequest.FeaturedImageUrl.IsNullOrEmpty() || 
                AddBlogPostRequest.PageTitle.IsNullOrEmpty() || AddBlogPostRequest.ShortDescription.IsNullOrEmpty() || 
                AddBlogPostRequest.UrlHandle.IsNullOrEmpty()) 
            {
                var notification = new Notification
                {
                    Type = Domain.Models.Enum.NotificationType.Error,
                    Message = "Complete all required fields!"
                    
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                ViewData["MessageValidation"] = "Complete all required fields!";
                return Page();
            }
            var newBlog = await _blogPostServices.AddAsync(AddBlogPostRequest);
            if (!newBlog.Success)
            {
                ViewData["Message"] = "Error";
                return Page();
            }
            var notification1 = new Notification 
            { 
                Type = Domain.Models.Enum.NotificationType.Success,
                Message = "New blog created!"
            };
            TempData["Notification"] = JsonSerializer.Serialize(notification1);
            return RedirectToPage("./List");
        }
    }
}
