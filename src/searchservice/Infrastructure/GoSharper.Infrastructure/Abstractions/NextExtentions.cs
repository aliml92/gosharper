using GoSharper.Infrastructure.Indices;
using Nest;

namespace GoSharper.Infrastructure.Abstractions
{
    public static class NestExtensions
    {
        public static QueryContainer BuildMultiMatchQuery<T>(string queryValue) where T : class
        {
            var fields = typeof(T).GetProperties().Select(p => p.Name.ToLower()).ToArray();

            return new QueryContainerDescriptor<T>()
                .MultiMatch(c => c
                    .Type(TextQueryType.Phrase)
                    .Fields(f => f.Fields(fields)).Lenient().Query(queryValue));
        }

        public static List<IndexPodcasts> GetSampleData()
        {
            var list = new List<IndexPodcasts>
            {
                new() {Id = "38d61273-8f4e-431c-a0bb-3de2fbbf8757", Title = "Summer Storm", Authors = "Cheyenne Velazquez", Description = "Vestibulum ut eros non enim commodo hendrerit. Donec porttitor tellus", Tags = "#jobseekers #careers #jobopening #opportunity ", Source = "http://dummyimage.com/159x100.png/ff4444/ffffff", AudioSource = "http://dummyimage.com/118x100.png/5fa2dd/ffffff", CreatedAt = DateTime.Now },
                new() {Id = "38d61273-8f4e-431c-a0bb-3de2fbbf8757", Title = "Special Forces", Authors = "Brennan Conner", Description = "Quisque ac libero nec ligula consectetuer rhoncus. Nullam velit dui", Tags = "#jobposting #internship #jobshiring", Source = "http://dummyimage.com/129x100.png/5fa2dd/ffffff", AudioSource = "http://dummyimage.com/228x100.png/5fa2dd/ffffff", CreatedAt = DateTime.Now },
                new() {Id = "38d61273-8f4e-431c-a0bb-3de2fbbf8757", Title = "Rise of the Machines", Authors = "Shelly Boyle", Description = "eget nisi dictum augue malesuada malesuada. Integer id magna et", Tags = "#imun #work #applynow #nowhiring ", Source = "http://dummyimage.com/210x100.png/dddddd/000000", AudioSource = "http://dummyimage.com/123x100.png/dddddd/000000", CreatedAt = DateTime.Now },
                new() {Id = "38d61273-8f4e-431c-a0bb-3de2fbbf8757", Title = "Almost Heroes", Authors = "Armand Chapman", Description = "libero lacus, varius et, euismod et, commodo at, libero. Morbi", Tags = "#imun #work #applynow #nowhiring ", Source = "http://dummyimage.com/236x100.png/5fa2dd/ffffff", AudioSource = "http://dummyimage.com/174x100.png/5fa2dd/ffffff", CreatedAt = DateTime.Now },
                new() {Id = "38d61273-8f4e-431c-a0bb-3de2fbbf8757", Title = "Vision Quest", Authors = "insley Shaffer", Description = "ligula. Aenean euismod mauris eu elit. Nulla facilisi. Sed neque", Tags = "#imun #work #applynow #nowhiring ", Source = "http://dummyimage.com/161x100.png/ff4444/ffffff", AudioSource = "http://dummyimage.com/129x100.png/dddddd/000000", CreatedAt = DateTime.Now },
                new() {Id = "38d61273-8f4e-431c-a0bb-3de2fbbf8757", Title = "The Enemy Within", Authors = "Nigel Chen", Description = "Quisque fringilla euismod enim. Etiam gravida molestie arcu. Sed", Tags = "#employment #jobalert #career", Source = "http://dummyimage.com/124x100.png/cc0000/ffffff", AudioSource = "http://dummyimage.com/193x100.png/cc0000/ffffff", CreatedAt = DateTime.Now },
                new() {Id = "38d61273-8f4e-431c-a0bb-3de2fbbf8757", Title = "The Legend of Ron Burgundy", Authors = "Adam Blake", Description = "Suspendisse sagittis. Nullam vitae diam. Proin dolor. Nulla semper tellus", Tags = "#international #jobsearching #unitednations", Source = "http://dummyimage.com/132x100.png/ff4444/ffffff", AudioSource = "http://dummyimage.com/201x100.png/dddddd/000000", CreatedAt = DateTime.Now },
                new() {Id = "38d61273-8f4e-431c-a0bb-3de2fbbf8757", Title = "Take the Lead", Authors = "April Green", Description = "orci, consectetuer euismod est arcu ac orci. Ut semper pretium", Tags = "#online #globalopportunity #modelunitednations", Source = "http://dummyimage.com/156x100.png/dddddd/000000", AudioSource = "http://dummyimage.com/119x100.png/ff4444/ffffff", CreatedAt = DateTime.Now },
                new() {Id = "38d61273-8f4e-431c-a0bb-3de2fbbf8757", Title = "Gifted Hands: The Ben Carson Story", Authors = "Clark Hurst", Description = "ligula. Aenean euismod mauris eu elit. Nulla facilisi. Sed neque", Tags = " #recruit #recruitment #youth #intern #un ", Source = "http://dummyimage.com/125x100.png/5fa2dd/ffffff", AudioSource = "http://dummyimage.com/145x100.png/5fa2dd/ffffff", CreatedAt = DateTime.Now },
                new() {Id = "38d61273-8f4e-431c-a0bb-3de2fbbf8757", Title = "Loving Story", Authors = "Tamekah Mcgee", Description = "scelerisque dui. Suspendisse ac metus vitae velit egestas lacinia. Sed", Tags = "#jobpost #hiring #job #jobs #jobsearch", Source = "http://dummyimage.com/176x100.png/5fa2dd/ffffff", AudioSource = "http://dummyimage.com/187x100.png/cc0000/ffffff", CreatedAt = DateTime.Now },
            };
            return list;
        }

        public static double ObterBucketAggregationDouble(AggregateDictionary agg, string bucket)
        {
            return agg.BucketScript(bucket).Value.HasValue ? agg.BucketScript(bucket).Value.Value : 0;
        }
    }
}
