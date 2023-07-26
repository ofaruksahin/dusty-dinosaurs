using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elasticsearch.API.Models.ECommerceModel;
using System.Collections.Immutable;

namespace Elasticsearch.API.Repositories
{
    public class ECommerceRepository
    {
        private ElasticsearchClient _client;
        private const string _indexName = "kibana_sample_data_ecommerce";

        public ECommerceRepository(ElasticsearchClient client)
        {
            _client = client;
        }


        public async Task<ImmutableList<ECommerce>> TermQuery(string customerFirstName)
        {
            //1. Way
            //var result = await _client.SearchAsync<ECommerce>(s => s.Index(_indexName).Query(q => q.Term(t => t.Field("customer_first_name.keyword").Value(customerFirstName))));

            //2. Way
            var result = await _client.SearchAsync<ECommerce>(s => s.Index(_indexName).Query(q => q.Term(t => t.CustomerFirstName.Suffix("keyword"), customerFirstName)));

            foreach (var item in result.Hits)
                item.Source.Id = item.Id;

            return result.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<ECommerce>> TermsQuery(List<string> customerFirstNameList)
        {
            List<FieldValue> terms = new List<FieldValue>();

            customerFirstNameList.ForEach(x => terms.Add(x));

            var termsQuery = new TermsQuery()
            {
                Field = "customer_first_name.keyword",
                Terms = new TermsQueryField(terms.AsReadOnly())
            };

            var result = await _client.SearchAsync<ECommerce>(s => s.Index(_indexName).Query(termsQuery));

            foreach (var item in result.Hits)
                item.Source.Id = item.Id;

            return result.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<ECommerce>> PrefixQuery(string customerFullName)
        {
            var result = await _client.SearchAsync<ECommerce>(s => s.Index(_indexName).Query(q => q.Prefix(p => p.Field(f => f.CustomerFullName.Suffix("keyword")).Value(customerFullName))));

            foreach (var item in result.Hits)
                item.Source.Id = item.Id;

            return result.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<ECommerce>> RangeQuery(double fromPrice, double toPrice)
        {
            var result = await _client.SearchAsync<ECommerce>(s => s.Index(_indexName).Query(q => q.Range(r => r.NumberRange(nr => nr.Field(f => f.TaxfulTotalPrice).Gte(fromPrice).Lte(toPrice)))));

            foreach (var item in result.Hits)
                item.Source.Id = item.Id;

            return result.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<ECommerce>> MatchAll()
        {
            var result = await _client.SearchAsync<ECommerce>(s =>
                s.Index(_indexName)
                .Size(100)
                .Query(q => q.MatchAll()));

            foreach (var item in result.Hits)
                item.Source.Id = item.Id;

            return result.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<ECommerce>> Paginate(int page, int pageSize)
        {
            int from = (page - 1) * pageSize;

            var result = await _client.SearchAsync<ECommerce>(s =>
                s.Index(_indexName)
                .Size(pageSize)
                .From(from)
                .Query(q => q.MatchAll()));

            foreach (var item in result.Hits)
                item.Source.Id = item.Id;

            return result.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<ECommerce>> WildCardQuery(string customerFullName)
        {
            var result = await _client.SearchAsync<ECommerce>(s =>
                s.Index(_indexName)
                .Query(q =>
                    q.Wildcard(w =>
                        w.Field(f => f.CustomerFullName.Suffix("keyword"))
                            .Wildcard(customerFullName))));

            foreach(var item in result.Hits)
                item.Source.Id = item.Id;   

            return result.Documents.ToImmutableList();
        }
    }
}
