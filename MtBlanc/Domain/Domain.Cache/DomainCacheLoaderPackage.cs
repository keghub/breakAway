using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakAway
{
    public class DomainCacheLoaderPackage : Studentum.Infrastructure.Caching.ReflectionCacheLoaderPackage
    {
        public override System.Reflection.Assembly GetAssembly()
        {
            return System.Reflection.Assembly.GetExecutingAssembly();
        }
    }
}
