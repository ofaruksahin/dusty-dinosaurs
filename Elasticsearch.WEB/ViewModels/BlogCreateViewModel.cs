using System.ComponentModel.DataAnnotations;

namespace Elasticsearch.WEB.ViewModels
{
    public class BlogCreateViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public List<string> Tags{ get; set; }

        public BlogCreateViewModel()
        {
            Tags = new List<string>();
        }
    }
}
