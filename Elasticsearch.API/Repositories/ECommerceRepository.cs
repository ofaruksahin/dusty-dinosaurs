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

            foreach (var item in result.Hits)
                item.Source.Id = item.Id;

            return result.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<ECommerce>> FuzzyQueryAsync(string customerName)
        {
            var result = await _client.SearchAsync<ECommerce>(s =>
                s.Index(_indexName)
                .Query(q =>
                    q.Fuzzy(fu =>
                        fu.Field(f => f.CustomerFirstName.Suffix("keyword"))
                            .Value(customerName)
                                .Fuzziness(new Fuzziness(2)))));

            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id;

            return result.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<ECommerce>> MatchQueryFullTextAsync(string categoryName)
        {
            var result = await _client.SearchAsync<ECommerce>(s =>
                s.Index(_indexName)
                    .Query(q =>
                        q.Match(m =>
                            m.Field(f =>
                                f.Category)
                                .Query(categoryName)
                                    .Operator(Operator.And))));

            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id;

            return result.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<ECommerce>> MatchBooleanPrefix(string customerFullName)
        {
            var result = await _client.SearchAsync<ECommerce>(s =>
                s.Index(_indexName)
                    .Query(q =>
                        q.MatchBoolPrefix(m =>
                            m.Field(f =>
                                f.CustomerFullName)
                                    .Query(customerFullName)
                                        .Operator(Operator.And))));

            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id;

            return result.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<ECommerce>> MatchPhrase(string customerFullName)
        {
            var result = await _client.SearchAsync<ECommerce>(s =>
                s.Index(_indexName)
                    .Query(q =>
                        q.MatchPhrase(m =>
                            m.Field(f =>
                                f.CustomerFullName)
                                    .Query(customerFullName))));

            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id;

            return result.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<ECommerce>> CompoundQueryExampleOne(
            string cityName,
            double taxfulTotalPrice,
            string categoryName,
            string manufacturer)
        {
            var result = await _client.SearchAsync<ECommerce>(s =>
                s.Index(_indexName)
                    .Query(q =>
                        q.Bool(b =>
                            b.Must(m =>
                                m.Term(c =>
                                    c.Field("geoip.city_name")
                                        .Value(cityName)))
                            .MustNot(mn =>
                                mn.Range(r =>
                                    r.NumberRange(nr =>
                                        nr
                                            .Field(f => f.TaxfulTotalPrice)
                                                .Lte(taxfulTotalPrice))))
                            .Should(s =>
                                s.Term(t =>
                                    t.Field(f =>
                                        f.Category.Suffix("keyword"))
                                            .Value(categoryName)))
                            .Filter(f =>
                                f.Term(t =>
                                    t.Field("manufacturer.keyword")
                                        .Value(manufacturer))))));

            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id;

            return result.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<ECommerce>> CompoundQueryExampleTwo(string customerFullName)
        {
            var result = await _client.SearchAsync<ECommerce>(s =>
                s.Index(_indexName)
                    .Query(q =>
                        q.Bool(b =>
                            b.Must(m =>
                                m.Match(m =>
                                    m.Field(f => f.CustomerFullName)
                                        .Query(customerFullName))
                                .Prefix(p => p.Field(
                                        f => f.CustomerFullName.Suffix("keyword")).Value(customerFullName))))));

            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id;

            return result.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<ECommerce>> MultiMatchQueryFullTextAsync(string name)
        {
            var result = await _client.SearchAsync<ECommerce>(s =>
                s.Index(_indexName)
                    .Query(q =>
                        q.MultiMatch(mm =>
                            mm.Fields(new Field("customer_first_name")
                                .And(new Field("customer_last_name"))
                                .And(new Field("customer_full_name"))).Query(name))));

            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id;

            return result.Documents.ToImmutableList();
        }
    }
}
