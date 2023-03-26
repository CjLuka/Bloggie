using Aplication.Interface;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BloggieWeb.Pages.Blog
{
    public class DetailsModel : PageModel
    {
        private readonly IBlogPostServices _blogPostServices;
        [BindProperty]
        public BlogPost BlogPost { get; set; }
        public DetailsModel(IBlogPostServices blogPostServices)
        {
            _blogPostServices = blogPostServices;
        }
        public async Task <IActionResult> OnGet(string urlHandle)
        {
            var blog = await _blogPostServices.GetDetailBlogPost(urlHandle);
            BlogPost = blog.Data;
            if (BlogPost == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
