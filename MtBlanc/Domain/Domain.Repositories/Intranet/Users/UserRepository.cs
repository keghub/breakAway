using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Studentum.Infrastructure.Repository;

namespace BreakAway.Domain.Intranet.Users
{
    public interface IUserRepository : IRepository<User>
    {
        IQueryable<Role> Roles { get; }
    }

    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IRepositoryProviderBase repositoryProvider) : base(repositoryProvider) { }

        public IQueryable<Role> Roles
        {
            get { return Provider.All<Role>(); }
        }
    }
}
