using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Studentum.Infrastructure.Caching;

namespace BreakAway.Domain.Intranet.Users
{
    public class Role : ICachedEntity
    {
        public int Id { get; protected set; }

        public int Key
        {
            get { return Id; }
        }

        public string Name { get; set; }
        
        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
