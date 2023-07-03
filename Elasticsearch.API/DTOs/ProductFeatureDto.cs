using Elasticsearch.API.Models;
using Elasticsearch.API.Models.Enums;

namespace Elasticsearch.API.DTOs
{
    public record ProductFeatureDto(
        int Width,
        int Height, 
        Color Color)
    {
        public ProductFeature CreateProductFeature()
        {
            return new ProductFeature
            {
                Width = Width,
                Height = Height,
                Color = Color
            };
        }
    }
}
