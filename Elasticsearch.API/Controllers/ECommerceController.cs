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
        public async Task<IActionResult> Paginate(int page,int pageSize)
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
    }
}
