using Aplication.Interface;
using Domain.Models.Domain;
using Domain.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

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


        //wyswietlenie postu ktory bedzie edytowany
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var response = await _blogPostServices.GetDetailBlogPost(id);
            BlogPost = response.Data;
            return Page();
        }
        //Updatewanie postu
        public async Task <IActionResult> OnPostUpdateAsync(Guid id)
        {

            if (BlogPost.Heading.IsNullOrEmpty() || BlogPost.Author.IsNullOrEmpty() ||
                BlogPost.Content.IsNullOrEmpty() || BlogPost.FeaturedImageUrl.IsNullOrEmpty() ||
                BlogPost.PageTitle.IsNullOrEmpty() || BlogPost.ShortDescription.IsNullOrEmpty() ||
                BlogPost.UrlHandle.IsNullOrEmpty())
            {
                ViewData["MessageValidation"] = "Complete all required fields!";
                return Page();
            }
            try
            {
                await _blogPostServices.UpdateAsync(BlogPost, id);
                ViewData["Notification"] = new Notification
                {
                    Message = "Record updated successfully",
                    Type = Domain.Models.Enum.NotificationType.Success
                };
            }
            catch (Exception ex)
            {
                ViewData["Notification"] = new Notification
                {
                    Message = "Something went wrong!",
                    Type = Domain.Models.Enum.NotificationType.Error
                };
            }
            
            //ViewData["MessageDescription"] = "Record was succesfully saved!";
            return Page();
            //return RedirectToPage("./List");
        }

        //Usuwanie postu
        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            var deleted = await _blogPostServices.DeleteAsync(id);
            if (deleted.Success)
            {
                var notification = new Notification
                {
                    Type = Domain.Models.Enum.NotificationType.Success,
                    Message= "Blog was deleted successfully!"
                };            
                TempData["Notification"] = JsonSerializer.Serialize(notification);

                return RedirectToPage("./List");
            }
            return Page();
            
        }
    }
}
