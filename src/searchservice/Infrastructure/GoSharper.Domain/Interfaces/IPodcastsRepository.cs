using GoSharper.Infrastructure.Elastic;
using GoSharper.Infrastructure.Indices;

namespace GoSharper.Domain.Interfaces
{
    public interface IPodcastsRepository : IElasticBaseRepository<IndexPodcasts>
    {
    }
}
