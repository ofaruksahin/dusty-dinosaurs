using Elasticsearch.API.DTOs;
using Elasticsearch.API.Models.ECommerceModel;
using Elasticsearch.API.Repositories;
using System.Collections.Immutable;
using System.Net;
using System.Runtime.CompilerServices;

namespace Elasticsearch.API.Services
{
    public class ECommerceService
    {
        private readonly ECommerceRepository _repository;

        public ECommerceService(ECommerceRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseDto<ImmutableList<ECommerce>>> TermQuery(string customerFirstName)
        {
            var eCommerceResponse = await _repository.TermQuery(customerFirstName);
            return ResponseDto<ImmutableList<ECommerce>>.Success(eCommerceResponse, HttpStatusCode.OK);
        }

        public async Task<ResponseDto<ImmutableList<ECommerce>>> TermsQuery(List<string> customerFirstNames)
        {
            var eCommerceResponse = await _repository.TermsQuery(customerFirstNames);
            return ResponseDto<ImmutableList<ECommerce>>.Success(eCommerceResponse, HttpStatusCode.OK);
        }

        public async Task<ResponseDto<ImmutableList<ECommerce>>> PrefixQuery(string customerFirstName)
        {
            var eCommerceResponse = await _repository.PrefixQuery(customerFirstName);
            return ResponseDto<ImmutableList<ECommerce>>.Success(eCommerceResponse, HttpStatusCode.OK);
        }

        public async Task<ResponseDto<ImmutableList<ECommerce>>> RangeQuery(double fromPrice, double toPrice)
        {
            var eCommerceResponse = await _repository.RangeQuery(fromPrice, toPrice);
            return ResponseDto<ImmutableList<ECommerce>>.Success(eCommerceResponse, HttpStatusCode.OK);
        }

        public async Task<ResponseDto<ImmutableList<ECommerce>>> MatchAll()
        {
            var eCommerceResponse = await _repository.MatchAll();
            return ResponseDto<ImmutableList<ECommerce>>.Success(eCommerceResponse, HttpStatusCode.OK);
        }

        public async Task<ResponseDto<ImmutableList<ECommerce>>> Paginate(int page, int pageSize)
        {
            var eCommerceResponse = await _repository.Paginate(page, pageSize);
            return ResponseDto<ImmutableList<ECommerce>>.Success(eCommerceResponse, HttpStatusCode.OK);
        }

        public async Task<ResponseDto<ImmutableList<ECommerce>>> WildCardQuery(string customerFullName)
        {
            var eCommerceResponse = await _repository.WildCardQuery(customerFullName);
            return ResponseDto<ImmutableList<ECommerce>>.Success(eCommerceResponse, HttpStatusCode.OK);
        }

        public async Task<ResponseDto<ImmutableList<ECommerce>>> FuzzyQuery(string customerFullName)
        {
            var eCommerceResponse = await _repository.FuzzyQueryAsync(customerFullName);
            return ResponseDto<ImmutableList<ECommerce>>.Success(eCommerceResponse, HttpStatusCode.OK);
        }

        public async Task<ResponseDto<ImmutableList<ECommerce>>> MatchQueryFullTextAsync(string categoryName)
        {
            var eCommerceResponse = await _repository.MatchQueryFullTextAsync(categoryName);
            return ResponseDto<ImmutableList<ECommerce>>.Success(eCommerceResponse, HttpStatusCode.OK);
        }
    }
}
