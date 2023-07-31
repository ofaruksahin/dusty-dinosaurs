using Elasticsearch.WEB.Models;
using Elasticsearch.WEB.Services;
using Elasticsearch.WEB.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Elasticsearch.WEB.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogService _blogService;

        public BlogController(BlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public IActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(BlogCreateViewModel viewModel)
        {
            await _blogService.SaveAsync(viewModel);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Search()
        {
            return View(await _blogService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchText)
        {
            var blogs = await _blogService.SearchAsync(searchText);
            return View(blogs);
        }
    }
}
