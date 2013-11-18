using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap.Configuration.DSL;
using Uniques.Library.Caching;
using Uniques.Library.Data;
using Uniques.Library.Users.Attributes;

namespace Uniques.Library.StructureMap
{
    public class UniqueRegistry : Registry
    {
        public UniqueRegistry()
        {
            For<ICacheRepository>().HybridHttpOrThreadLocalScoped().Add<HttpContextCacheRepository>().Named("Immediate");
            For<ICacheRepository>().HybridHttpOrThreadLocalScoped().Add<SessionCacheRepository>().Named("Personal");
            For<ICacheRepository>().Singleton().Add<ThreadCacheRepository>().Named("Singleton");

            For<UniquesDataContext>().HybridHttpOrThreadLocalScoped().Use<UniquesDataContext>();

            For<UserAttributeManager>().Singleton().Use<UserAttributeManager>()
                .Ctor<Func<UniquesDataContext>>("dbContextGetter").Is(ctx => ctx.GetInstance<UniquesDataContext>)
                .Ctor<ICacheRepository>("cache").Is(ctx => ctx.GetInstance<ICacheRepository>("Singleton"));
        }
    }
}
