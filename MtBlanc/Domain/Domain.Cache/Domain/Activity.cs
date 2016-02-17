using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Studentum.Infrastructure.Caching;
using Studentum.Infrastructure.Repository;

namespace BreakAway.Domain
{
    public class ActivityLoader : CachedEntityLoader<Activity, int>
    {
        public ActivityLoader() : base("BreakAway") { }

        protected override IEnumerable<Activity> LoadAll()
        {
            IRepository<Activity> repository = RepositoryFactory.GetReadOnlyRepository<ActivityRepository>();
            return repository.Items;
        }

        protected override Activity LoadItem(int key)
        {
            IRepository<Activity> repository = RepositoryFactory.GetReadOnlyRepository<ActivityRepository>();
            Activity item = repository.Get(key);
            return item;
        }
    }

    public class ActivityStore : LocalStoreBase<Activity, int>
    {
        public ActivityStore(ILoader<Activity, int> loader) : base(loader, null) { }
    }
}
