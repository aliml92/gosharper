using GoSharper.Infrastructure.Indices;

namespace GoSharper.Domain.Interfaces
{
    public interface IPodcastsApplication
    {
        Task InsertManyAsync();
        Task<ICollection<IndexPodcasts>> GetAllAsync();
        Task<ICollection<IndexPodcasts>> GetByTitleWithTerm(string title);
        Task<ICollection<IndexPodcasts>> GetByTitleWithMatch(string title);
        Task<ICollection<IndexPodcasts>> GetByTitleAndDescriptionMultiMatch(string term);
        Task<ICollection<IndexPodcasts>> GetByTitleWithMatchPhrase(string title);
        Task<ICollection<IndexPodcasts>> GetByTitleWithMatchPhrasePrefix(string title);
        Task<ICollection<IndexPodcasts>> GetByTitleWithWildcard(string title);
        Task<ICollection<IndexPodcasts>> GetByTitleWithFuzzy(string title);
        Task<ICollection<IndexPodcasts>> SearchInAllFiels(string term);
        Task<ICollection<IndexPodcasts>> GetByDescriptionMatch(string description);
        Task<ICollection<IndexPodcasts>> GetPodcastsCondition(string title, string description, DateTime? createdDate);
        Task<ICollection<IndexPodcasts>> GetPodcastsAllCondition(string term);
        //Task<PodcastsAggregationModel> GetPodcastsAggregation();
    }
}
