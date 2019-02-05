using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Studentum.Domain;
using Studentum.Infrastructure.Caching;

namespace BreakAway.Domain.Core.Sites
{
    public class Site : ICachedEntity
    {
        public int Id { get; protected set; }

        public int Key
        {
            get { return Id; }
        }

        public string Domain { get; set; }

        public bool IsActive { get; set; }

        public virtual SiteType Type { get; set; }

        public  int TypeId { get; set; }


        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
