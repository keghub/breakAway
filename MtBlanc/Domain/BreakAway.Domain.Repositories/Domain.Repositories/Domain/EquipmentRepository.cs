using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Studentum.Infrastructure.Repository;

namespace BreakAway.Domain
{
    [Repository(AggregateName="BreakAway")]
    public class EquipmentRepository : RepositoryBase<Equipment>, IRepository<Equipment>
    {
        public EquipmentRepository(IRepositoryProviderBase repositoryProvider) : base(repositoryProvider) { }
    }
}
