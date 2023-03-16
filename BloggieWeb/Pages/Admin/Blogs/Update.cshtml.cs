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
        public async Task <IActionResult> OnPostAsync(Guid id, BlogPost blogPost)
        {
            var blogFromBase = await _blogPostServices.GetDetailBlogPost(blogPost.id);
            if (blogFromBase.Data.id == id)
            {
                blogFromBase.Data.Heading = blogPost.Heading;
                blogFromBase.Data.PublishedDate = blogPost.PublishedDate;
                blogFromBase.Data.PageTitle = blogPost.PageTitle;
                blogFromBase.Data.Author = blogPost.Author;
                blogFromBase.Data.Content = blogPost.Content;
                blogFromBase.Data.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                blogFromBase.Data.Visible = blogPost.Visible;
                blogFromBase.Data.UrlHandle = blogPost.UrlHandle;
                blogFromBase.Data.ShortDescription = blogPost.ShortDescription;
                await _blogPostServices.UpdateAsync(blogFromBase.Data);
            }
            //var response = await _blogPostServices.UpdateAsync(id);
            //BlogPost = response.Data;
            return RedirectToPage("./List");
        }
    }
}
