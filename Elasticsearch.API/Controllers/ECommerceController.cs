using Elasticsearch.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Elasticsearch.API.Controllers
{
    public class ECommerceController : BaseController
    {
        private readonly ECommerceService _eCommerceService;

        public ECommerceController(ECommerceService eCommerceService)
        {
            _eCommerceService = eCommerceService;
        }

        [HttpGet("term-query")]
        public async Task<IActionResult> TermQuery(string customerFirstName)
        {
            return CreateActionResult(await _eCommerceService.TermQuery(customerFirstName));
        }

        [HttpPost("terms-query")]
        public async Task<IActionResult> TermsQuery(List<string> customerFirstNames)
        {
            return CreateActionResult(await _eCommerceService.TermsQuery(customerFirstNames));
        }

        [HttpGet("prefix-query")]
        public async Task<IActionResult> PrefixQuery(string prefix)
        {
            return CreateActionResult(await _eCommerceService.PrefixQuery(prefix));
        }

        [HttpGet("range-query")]
        public async Task<IActionResult> RangeQuery(double fromPrice, double toPrice)
        {
            return CreateActionResult(await _eCommerceService.RangeQuery(fromPrice, toPrice));
        }

        [HttpGet("match-all")]
        public async Task<IActionResult> MatchAll()
        {
            return CreateActionResult(await _eCommerceService.MatchAll());
        }

        [HttpGet("paginate")]
        public async Task<IActionResult> Paginate(int page, int pageSize)
        {
            return CreateActionResult(await _eCommerceService.Paginate(page, pageSize));
        }

        [HttpGet("wildcard")]
        public async Task<IActionResult> WildCard(string customerFullName)
        {
            return CreateActionResult(await _eCommerceService.WildCardQuery(customerFullName));
        }

        [HttpGet("fuzzy")]
        public async Task<IActionResult> Fuzzy(string customerFullName)
        {
            return CreateActionResult(await _eCommerceService.FuzzyQuery(customerFullName));
        }

        [HttpGet("match-query-full-text")]
        public async Task<IActionResult> MatchQueryFullText(string categoryName)
        {
            return CreateActionResult(await _eCommerceService.MatchQueryFullTextAsync(categoryName));
        }

        [HttpGet("match-boolean-prefix")]
        public async Task<IActionResult> MatchBooleanPrefix(string customerFullName)
        {
            return CreateActionResult(await _eCommerceService.MatchBooleanPrefix(customerFullName));
        }

        [HttpGet("match-phrase")]
        public async Task<IActionResult> MatchPhrase(string customerFullName)
        {
            return CreateActionResult(await _eCommerceService.MatchPhrase(customerFullName));
        }

        [HttpGet("compound-query-example-one")]
        public async Task<IActionResult> CompoundQueryExampleOne(
            string cityName,
            double taxfulTotalPrice,
            string categoryName,
            string manufacturer)
        {
            return CreateActionResult(await _eCommerceService.CompoundQueryExampleOne(cityName, taxfulTotalPrice, categoryName, manufacturer));
        }

        [HttpGet("compound-query-example-two")]
        public async Task<IActionResult> CompoundQueryExampleTwo(string customerFullName)
        {
            return CreateActionResult(await _eCommerceService.CompoundQueryExampleTwo(customerFullName));
        }

        [HttpGet("multi-match-query")]
        public async Task<IActionResult> MultiMatchQuery(string customerName)
        {
            return CreateActionResult(await _eCommerceService.MultiMatchQueryFullTextAsync(customerName));
        }
    }
}
