using Elasticsearch.WEB.Models;
using Elasticsearch.WEB.Repositories;
using Elasticsearch.WEB.ViewModels;

namespace Elasticsearch.WEB.Services
{
    public class BlogService
    {
        private readonly BlogRepository _blogRepository;

        public BlogService(BlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<bool> SaveAsync(BlogCreateViewModel viewModel)
        {
            var blog = new Blog
            {
                Title = viewModel.Title,
                Content = viewModel.Content,
                Tags = viewModel.Tags.Split(',').ToList(),
                UserId = Guid.NewGuid()
            };

            return await _blogRepository.SaveAsync(blog);
        }
    }
}
