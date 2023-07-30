using Elastic.Clients.Elasticsearch;
using Elasticsearch.WEB.Models;

namespace Elasticsearch.WEB.Repositories
{
    public class BlogRepository
    {
        private readonly ElasticsearchClient _client;

        private readonly string _indexName = "blog";

        public BlogRepository(ElasticsearchClient client)
        {
            _client = client;
        }

        public async Task<bool> SaveAsync(Blog newBlog)
        {
            newBlog.Created = DateTime.Now;

            var response = await _client.IndexAsync(newBlog, f => f.Index(_indexName));

            if (!response.IsValidResponse) return false;

            newBlog.Id = response.Id;

            return true;
        }
    }
}
