using Elasticsearch.API.Models;

namespace Elasticsearch.API.DTOs
{
    public record ProductUpdateDto(
        string Name,
        decimal Price,
        int Stock,
        ProductFeatureDto ProductFeature)
    {
        public Product CreateProduct()
        {
            return new Product
            {
                Name = Name,
                Price = Price,
                Stock = Stock,
                Feature = ProductFeature.CreateProductFeature()
            };
        }
    }
}
