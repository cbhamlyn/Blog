using Admin.Data;
using Admin.Models;
using Admin.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BlogDbContext blogDbContext;

        public HomeController(ILogger<HomeController> logger, BlogDbContext blogDbContext)
        {
            _logger = logger;
            this.blogDbContext = blogDbContext;
        }

        public async Task<IActionResult> Index()
        {
            ListRequest request = new ListRequest();
            request.Categories = blogDbContext.Categories.ToList();
            request.Posts = blogDbContext.BlogPosts.ToList();

            return View(request);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}