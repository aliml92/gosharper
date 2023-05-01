using GoSharper.Domain.Interfaces;
using GoSharper.Infrastructure.Abstractions;
using GoSharper.Infrastructure.Indices;
using Nest;

namespace GoSharper.Domain.Applications
{
    public class PodcastsApplication : IPodcastsApplication
    {
        private readonly IPodcastsRepository _podcastsRepository;

        public PodcastsApplication(IPodcastsRepository podcastsRepository)
        {
            _podcastsRepository = podcastsRepository;
        }

        public async Task InsertManyAsync()
        {
            await _podcastsRepository.InsertManyAsync(NestExtensions.GetSampleData());
        }

        public async Task<ICollection<IndexPodcasts>> GetAllAsync()
        {
            var result = await _podcastsRepository.GetAllAsync();

            return result.ToList();
        }

        //lowcase
        public async Task<ICollection<IndexPodcasts>> GetByTitleWithTerm(string name)
        {
            var query = new QueryContainerDescriptor<IndexPodcasts>().Term(p => p.Field(p => p.Title).Value(name).CaseInsensitive().Boost(6.0));
            var result = await _podcastsRepository.SearchAsync(_ => query);

            return result?.ToList();
        }

        //using operator OR in case insensitive
        public async Task<ICollection<IndexPodcasts>> GetByTitleWithMatch(string title)
        {
            var query = new QueryContainerDescriptor<IndexPodcasts>().Match(p => p.Field(f => f.Title).Query(title).Operator(Operator.And));
            var result = await _podcastsRepository.SearchAsync(_ => query);

            return result?.ToList();
        }

        public async Task<ICollection<IndexPodcasts>> GetByTitleWithMatchPhrase(string title)
        {
            var query = new QueryContainerDescriptor<IndexPodcasts>().MatchPhrase(p => p.Field(f => f.Title).Query(title));
            var result = await _podcastsRepository.SearchAsync(_ => query);

            return result?.ToList();
        }

        public async Task<ICollection<IndexPodcasts>> GetByTitleWithMatchPhrasePrefix(string title)
        {
            var query = new QueryContainerDescriptor<IndexPodcasts>().MatchPhrasePrefix(p => p.Field(f => f.Title).Query(title));
            var result = await _podcastsRepository.SearchAsync(_ => query);

            return result?.ToList();
        }

        //contains
        public async Task<ICollection<IndexPodcasts>> GetByTitleWithWildcard(string title)
        {
            var query = new QueryContainerDescriptor<IndexPodcasts>().Wildcard(w => w.Field(f => f.Title).Value($"*{title}*").CaseInsensitive());
            var result = await _podcastsRepository.SearchAsync(_ => query);

            return result?.ToList();
        }

        public async Task<ICollection<IndexPodcasts>> GetByTitleWithFuzzy(string title)
        {
            var query = new QueryContainerDescriptor<IndexPodcasts>().Fuzzy(descriptor => descriptor.Field(p => p.Title).Value(title));
            var result = await _podcastsRepository.SearchAsync(_ => query);

            return result?.ToList();
        }

        public async Task<ICollection<IndexPodcasts>> SearchInAllFiels(string term)
        {
            var query = NestExtensions.BuildMultiMatchQuery<IndexPodcasts>(term);
            var result = await _podcastsRepository.SearchAsync(_ => query);

            return result.ToList();
        }

        public async Task<ICollection<IndexPodcasts>> GetByDescriptionMatch(string description)
        {
            //case insensitive
            var query = new QueryContainerDescriptor<IndexPodcasts>().Match(p => p.Field(f => f.Description).Query(description));
            var result = await _podcastsRepository.SearchAsync(_ => query);

            return result?.ToList();
        }

        public async Task<ICollection<IndexPodcasts>> GetByTitleAndDescriptionMultiMatch(string term)
        {
            var query = new QueryContainerDescriptor<IndexPodcasts>()
                .MultiMatch(p => p.
                    Fields(p => p.
                        Field(f => f.Title).
                        Field(d => d.Description)).
                    Query(term).Operator(Operator.And));

            var result = await _podcastsRepository.SearchAsync(_ => query);

            return result?.ToList();
        }

        public async Task<ICollection<IndexPodcasts>> GetPodcastsCondition(string title, string description, DateTime? createdDate)
        {
            QueryContainer query = new QueryContainerDescriptor<IndexPodcasts>();

            if (!string.IsNullOrEmpty(title))
            {
                query = query && new QueryContainerDescriptor<IndexPodcasts>().MatchPhrasePrefix(qs => qs.Field(fs => fs.Title).Query(title));
            }
            if (!string.IsNullOrEmpty(description))
            {
                query = query && new QueryContainerDescriptor<IndexPodcasts>().MatchPhrasePrefix(qs => qs.Field(fs => fs.Description).Query(description));
            }
            if (createdDate.HasValue)
            {
                query = query && new QueryContainerDescriptor<IndexPodcasts>()
                .Bool(b => b.Filter(f => f.DateRange(dt => dt
                                           .Field(field => field.CreatedAt)
                                           .GreaterThanOrEquals(createdDate)
                                           .LessThanOrEquals(createdDate)
                                           .TimeZone("+00:00"))));
            }

            var result = await _podcastsRepository.SearchAsync(_ => query);

            return result?.ToList();
        }

        public async Task<ICollection<IndexPodcasts>> GetPodcastsAllCondition(string term)
        {
            var query = new QueryContainerDescriptor<IndexPodcasts>().Bool(b => b.Must(m => m.Exists(e => e.Field(f => f.Description))));
            int.TryParse(term, out var numero);

            query = query && new QueryContainerDescriptor<IndexPodcasts>().Wildcard(w => w.Field(f => f.Title).Value($"*{term}*")) //bad performance, use MatchPhrasePrefix
                    || new QueryContainerDescriptor<IndexPodcasts>().Wildcard(w => w.Field(f => f.Authors).Value($"*{term}*")) //bad performance, use MatchPhrasePrefix
                    || new QueryContainerDescriptor<IndexPodcasts>().Wildcard(w => w.Field(f => f.Description).Value($"*{term}*")) //bad performance, use MatchPhrasePrefix
                    || new QueryContainerDescriptor<IndexPodcasts>().Wildcard(w => w.Field(f => f.Tags).Value($"*{term}*")); //bad performance, use MatchPhrasePrefix
                                                                                                                             //|| new QueryContainerDescriptor<IndexPodcasts>().Term(w => w.TotalMovies, numero);

            var result = await _podcastsRepository.SearchAsync(_ => query);

            return result?.ToList();
        }

        //public async Task<PodcastsAggregationModel> GetPodcastsAggregation()
        //{
        //    var query = new QueryContainerDescriptor<IndexPodcasts>().Bool(b => b.Must(m => m.Exists(e => e.Field(f => f.Description))));

        //    var result = await _podcastsRepository.SearchAsync(_ => query, a =>
        //                a.Sum("TotalAge", sa => sa.Field(o => o.Age))
        //                .Sum("TotalMovies", sa => sa.Field(p => p.TotalMovies))
        //                .Average("AvAge", sa => sa.Field(p => p.Age)));

        //    var totalAge = NestExtensions.ObterBucketAggregationDouble(result.Aggregations, "TotalAge");
        //    var totalMovies = NestExtensions.ObterBucketAggregationDouble(result.Aggregations, "TotalMovies");
        //    var avAge = NestExtensions.ObterBucketAggregationDouble(result.Aggregations, "AvAge");

        //    return new PodcastsAggregationModel { TotalAge = totalAge, TotalMovies = totalMovies, AverageAge = avAge };
        //}
    }
}
