using System.Collections.Generic;
using System.Reflection;
using BreakAway.Domain;
using Studentum.Infrastructure.Repository;

namespace BreakAway
{
    public class ModelMapperPackage : Studentum.Infrastructure.Repository.IModelMapperPackage
    {
        public IEnumerable<IEntityModelMapper> GetMappers()
        {
            yield return new DomainModelMapper();
        }
    }

    public class DomainCacheModelMappingPackage : ReflectionCacheModelMappingPackage
    {
        public override System.Reflection.Assembly GetAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }
    }
}