using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BreakAway.Domain.Core.Sites;
using Studentum.Infrastructure.Caching;
using Studentum.Infrastructure.Collections;

namespace BreakAway.Domain.Intranet.Users
{
    public class User : ICachedEntity
    {
        public int Id { get; protected set; }

        public int Key
        {
            get { return Id; }
        }

        public string Name { get; set; }

        public string Email { get; set; }

        public int SiteId { get; set; }

        public virtual UserSite Site { get; set; }

        public virtual EntityCollection<User, Role> Roles { get; set; }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
