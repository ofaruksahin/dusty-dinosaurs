using Elasticsearch.API.DTOs;
using Elasticsearch.API.Models.Enums;

namespace Elasticsearch.API.Models
{
    public class ProductFeature
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Color Color { get; set; }

        public ProductFeatureDto CreateDto()
        {
            return new ProductFeatureDto(Width, Height, Color);
        }
    }
}
