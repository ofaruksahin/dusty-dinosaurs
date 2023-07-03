using Elasticsearch.Net;
using Nest;

namespace Elasticsearch.API.Extensions
{
    public static class ElasticsearchExtensions
    {
        public static void AddElasticClient(this IServiceCollection services, IConfiguration configuration)
        {
            var pool = new SingleNodeConnectionPool(new Uri(configuration.GetSection("Elastic")["Url"]!));
            var settings = new ConnectionSettings(pool);
            //settings.BasicAuthentication("","");
            var client = new ElasticClient(settings);

            services.AddSingleton<ElasticClient>(client);
        }
    }
}
