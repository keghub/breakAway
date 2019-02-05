using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel;

namespace BreakAway.DependencyInjection
{
    public class WindsorDependencyResolver : Studentum.Infrastructure.Utilities.IDependencyResolver
    {
        private readonly IKernel _kernel;

        public WindsorDependencyResolver(IKernel kernel)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException("kernel");
            }
            _kernel = kernel;
        }

        public object Resolve(Type type)
        {
            return _kernel.Resolve(type);
        }

        public IEnumerable<object> ResolveAll(Type type)
        {
            return _kernel.ResolveAll(type).Cast<object>();
        }

        public void Release<T>(T obj)
        {
            _kernel.ReleaseComponent(obj);
        }
    }
}
