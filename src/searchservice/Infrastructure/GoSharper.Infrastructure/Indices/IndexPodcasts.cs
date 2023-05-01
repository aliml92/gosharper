namespace GoSharper.Infrastructure.Indices
{
    public class IndexPodcasts : ElasticBaseIndex
    {
        public string Title { get; set; }
        public string Authors { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public string Source { get; set; }
        public string AudioSource { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
