using Aplication.Interface;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace BloggieWeb.Pages.Admin.Blogs
{
    public class ListModel : PageModel
    {
        private readonly IBlogPostServices _blogPostServices;
        public List<BlogPost> BlogPosts { get; set; }
        public ListModel(IBlogPostServices blogPostServices)
        {
            _blogPostServices = blogPostServices;
        }
        //[BindProperty]
        //BlogPost BlogPost { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var allBlogs = await _blogPostServices.GetAllAsync();
            BlogPosts = allBlogs.Data.ToList();
            if(BlogPosts == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
