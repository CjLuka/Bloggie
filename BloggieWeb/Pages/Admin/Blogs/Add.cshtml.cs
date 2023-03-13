using Aplication.Interface;
using Domain.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistance.Data;

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
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
            
            if (AddBlogPostRequest.Heading.IsNullOrEmpty()) 
            {
                ViewData["Message"] = "Produkt jest nullem";
                return Page();
            }
            var newBlog = await _blogPostServices.AddAsync(AddBlogPostRequest);
            if (!newBlog.Success)
            {
                ViewData["Message"] = "Blad";
                return Page();
            }
            return RedirectToPage("../index");
        }
    }
}
