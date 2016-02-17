using Studentum.Infrastructure.Repository;

namespace BreakAway.Domain
{
    public class DomainCacheModelMapper : ICacheModelMapper
    {
        public string AggregateName
        {
            get { return "BreakAway"; }
        }

        public CacheModelMapping Create()
        {
            CacheModelMapping modelMapping = new CacheModelMapping();

            var activity = modelMapping.Entity<Activity>();

            return modelMapping;
        }
    }
}