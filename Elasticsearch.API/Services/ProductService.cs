using Elasticsearch.API.DTOs;
using Elasticsearch.API.Repositories;
using System.Collections.Immutable;
using System.Net;

namespace Elasticsearch.API.Services
{
    public class ProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly ProductRepository _productRepository;

        public ProductService(
            ILogger<ProductService> logger,
            ProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task<ResponseDto<ProductDto>> SaveAsync(ProductCreateDto request)
        {
            var product = request.CreateProduct();

            var response = await _productRepository.SaveAsync(product);

            if (response == null)
                return ResponseDto<ProductDto>.Fail(new List<string> { "Kayıt esnasında bir hata meydana geldi." }, HttpStatusCode.InternalServerError);

            return ResponseDto<ProductDto>.Success(response.CreateDto(), HttpStatusCode.Created);
        }

        public async Task<ResponseDto<ImmutableList<ProductDto>>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();

            return ResponseDto<ImmutableList<ProductDto>>.Success(products.Select(f => f.CreateDto()).ToImmutableList(), HttpStatusCode.OK);
        }

        public async Task<ResponseDto<ProductDto>> GetByIdAsync(string id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product is null)
                return ResponseDto<ProductDto>.Fail(new List<string> { "Kayıt bulunamadı" }, HttpStatusCode.NotFound);
            return ResponseDto<ProductDto>.Success(product.CreateDto(), HttpStatusCode.OK);
        }

        public async Task<ResponseDto<NoContentDto>> UpdateAsync(string id,ProductUpdateDto request)
        {
            var response = await _productRepository.UpdateAsync(id,request.CreateProduct());
            if (!response)
                return ResponseDto<NoContentDto>.Fail(new List<string> { "Kayıt güncellenemedi" }, HttpStatusCode.InternalServerError);
            return ResponseDto<NoContentDto>.Success(new NoContentDto(), HttpStatusCode.OK);
        }

        public async Task<ResponseDto<NoContentDto>> DeleteAsync(string id)
        {
            var deleteResponse = await _productRepository.DeleteAsync(id);

            if (!deleteResponse.IsValid && deleteResponse.Result == Nest.Result.NotFound)
                return ResponseDto<NoContentDto>.Fail(new List<string> { "Silmeye çalıştığınız ürün bulunamamıştır" }, HttpStatusCode.NotFound);           

            if (!deleteResponse.IsValid)
            {
                _logger.LogError(deleteResponse.OriginalException, deleteResponse.ServerError.ToString());

                return ResponseDto<NoContentDto>.Fail(new List<string> { "Kayıt silinemedi" }, HttpStatusCode.InternalServerError);
            }
            return ResponseDto<NoContentDto>.Success(new NoContentDto(), HttpStatusCode.OK);
        }
    }
}
