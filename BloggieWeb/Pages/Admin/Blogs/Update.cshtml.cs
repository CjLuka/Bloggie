using Aplication.Interface;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BloggieWeb.Pages.Admin.Blogs
{
    public class UpdateModel : PageModel
    {
        private readonly IBlogPostServices _blogPostServices;
        public UpdateModel(IBlogPostServices blogPostServices)
        {
            _blogPostServices = blogPostServices;
        }

        [BindProperty]
        public BlogPost BlogPost { get; set; }
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var response = await _blogPostServices.GetDetailBlogPost(id);
            BlogPost = response.Data;
            return Page();
        }
        public async Task <IActionResult> OnPostUpdateAsync(Guid id)
        {
            await _blogPostServices.UpdateAsync(BlogPost, id);
            return RedirectToPage("./List");
        }
        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            await _blogPostServices.DeleteAsync(id);
            return RedirectToPage("./List");
        }
    }
}
