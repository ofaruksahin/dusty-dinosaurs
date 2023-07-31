using System.ComponentModel.DataAnnotations;

namespace Elasticsearch.WEB.ViewModels
{
    public class BlogCreateViewModel
    {
        [Required]
        [Display(Name = "Blog Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Blog Content")]
        public string Content { get; set; }

        [Display(Name = "Blog Tags")]
        public string Tags{ get; set; }
    }
}
