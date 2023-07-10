using Elastic.Clients.Elasticsearch;

namespace Elasticsearch.API.Extensions
{
    public static class ElasticsearchExtensions
    {
        public static void AddElasticClient(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = new ElasticsearchClientSettings(new Uri(configuration.GetSection("Elastic")["Url"]!));
            //settings.Authentication(new BasicAuthentication("", ""));

            var client = new ElasticsearchClient(settings);

            services.AddSingleton<ElasticsearchClient>(client);
        }
    }
}
