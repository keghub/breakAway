using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Studentum.Infrastructure.Repository;

namespace BreakAway.Domain
{
    [Repository(AggregateName = "BreakAway")]
    public class ActivityRepository : RepositoryBase<Activity>, IRepository<Activity>
    {
        public ActivityRepository(IRepositoryProviderBase repositoryProvider) : base(repositoryProvider) { }
    }
}
