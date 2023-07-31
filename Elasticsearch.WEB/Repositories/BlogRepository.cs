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

        public async Task<List<Blog>> SearchAsync(string searchText)
        {
            var response = await _client.SearchAsync<Blog>(s =>
                s.Index(_indexName)
                .Query(q =>
                    q.Bool(b =>
                        b.Should(s =>
                            s.Match(m =>
                                m.Field(f => f.Content)
                                .Query(searchText)),
                            s => s.MatchBoolPrefix(m =>
                                m.Field(f => f.Title)
                                .Query(searchText))))));

            foreach (var item in response.Hits)
                item.Source.Id = item.Id;

            return response.Documents.ToList();
        }

        public async Task<List<Blog>> GetAllAsync()
        {
            var response = await _client.SearchAsync<Blog>(s =>
                s.Index(_indexName)
                .From(0)
                .Size(100));

            foreach (var item in response.Hits)
                item.Source.Id = item.Id;

            return response.Documents.ToList();
        }
    }
}
