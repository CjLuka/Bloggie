using Aplication.Interface;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Persistance.Data;

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
        public async Task <IActionResult> OnGetAsync(Guid id)
        {
            var response = await _blogPostServices.UpdateAsync(id);
            BlogPost = response.Data;
            return Page();
        }
        //public async Task<IActionResult> OnPostAsync(Guid id)
        //{
        //    BlogPost = _blogPostServices.UpdateAsync(id);
        //    return RedirectToPage("/List");
        //}
    }
}
