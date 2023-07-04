using Elasticsearch.API.DTOs;
using Elasticsearch.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Elasticsearch.API.Controllers
{
        public class ProductsController : BaseController
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCreateDto request)
        {
            var response = await _productService.SaveAsync(request);
            return CreateActionResult(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _productService.GetAllAsync();
            return CreateActionResult(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _productService.GetByIdAsync(id);
            return CreateActionResult(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody]ProductUpdateDto request)
        {
            var response = await _productService.UpdateAsync(id,request);
            return CreateActionResult(response);
        }
    }
}
