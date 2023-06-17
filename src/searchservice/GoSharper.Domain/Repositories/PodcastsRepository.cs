using GoSharper.Domain.Interfaces;
using GoSharper.Infrastructure.Elastic;
using GoSharper.Infrastructure.Indices;
using Nest;

namespace GoSharper.Domain.Repositories
{
    public class PodcastsRepository : ElasticBaseRepository<IndexPodcasts>, IPodcastsRepository
    {
        public PodcastsRepository(IElasticClient elasticClient)
            : base(elasticClient)
        {
        }

        public override string IndexName { get; } = "indexpodcasts";
    }
}
