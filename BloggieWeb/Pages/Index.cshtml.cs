using Aplication.Interface;
using Domain.Models.Domain;
using Domain.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Persistance.Interfaces;
using System.Text.Json;

namespace BloggieWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBlogPostServices _blogPostServices;

        public IndexModel(ILogger<IndexModel> logger, IBlogPostServices blogPostServices)
        {
            _logger = logger;
            _blogPostServices = blogPostServices;
        }

        [BindProperty]
        public List<BlogPost> BlogPosts { get; set; }
        public async Task <IActionResult> OnGet()
        {
            var allBlogs = await _blogPostServices.GetAllAsync();
            BlogPosts = allBlogs.Data.ToList();
            if (BlogPosts == null)
            {
                return NotFound();
            }
            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson.ToString());
            }
            return Page();
        }
    }
}