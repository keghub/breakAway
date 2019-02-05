using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Studentum.Infrastructure.Repository;

namespace BreakAway.Domain.Core.Sites
{
    public interface ISiteRepository : IRepository<Site>
    {
        IQueryable<Site> GetMainSites();
    }

    public class SiteRepository : RepositoryBase<Site>, ISiteRepository
    {
        private const string MainSiteName = "Main Site";

        public SiteRepository(IRepositoryProviderBase repositoryProvider) : base(repositoryProvider) { }

        public IQueryable<Site> GetMainSites()
        {
            return Items.Where(p => p.Type.Name == MainSiteName);
        }
    }
}
