using System.Text.Json.Serialization;

namespace Elasticsearch.WEB.Models
{
    public class Blog
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }
        [JsonPropertyName("user_id")]
        public Guid UserId { get; set; }
        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        public Blog()
        {
            Tags = new List<string>();
        }
    }
}
