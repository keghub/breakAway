using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Studentum.Infrastructure.Repository;

namespace BreakAway.Domain
{
    [Repository(AggregateName = "BreakAway")]
    public class CustomerRepository : RepositoryBase<Customer>, IRepository<Customer>
    {
        public CustomerRepository(IRepositoryProviderBase repositoryProvider) : base(repositoryProvider) { }
    }
}
