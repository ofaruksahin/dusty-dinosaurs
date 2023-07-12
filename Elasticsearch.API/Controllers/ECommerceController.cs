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
    }
}
