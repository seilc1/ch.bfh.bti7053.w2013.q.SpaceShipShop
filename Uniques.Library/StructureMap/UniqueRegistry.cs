using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using StructureMap.Configuration.DSL;
using Uniques.Library.Authentication;
using Uniques.Library.Caching;
using Uniques.Library.Data;
using Uniques.Library.Users;
using Uniques.Library.Users.Attributes;
using Uniques.Library.Users.Images;
using Uniques.Library.Users.SimpleSearch;

namespace Uniques.Library.StructureMap
{
	public class UniqueRegistry : Registry
	{
		public UniqueRegistry()
		{
			// db context.
			For<UniquesDataContext>().HybridHttpOrThreadLocalScoped().Use<UniquesDataContext>();
			For<Func<UniquesDataContext>>().Use(ctx => ctx.GetInstance<UniquesDataContext>);

			// cache.
			For<ICacheRepository>().HybridHttpOrThreadLocalScoped().Add<HttpContextCacheRepository>().Named("Immediate");
			For<ICacheRepository>().HybridHttpOrThreadLocalScoped().Add<SessionCacheRepository>().Named("Personal");
			For<ICacheRepository>().Singleton().Add<ThreadCacheRepository>().Named("Singleton");
			
			// user bindings.
			For<UserManager>().Use<UserManager>();
			For<UserAttributeManager>().Use<UserAttributeManager>()
				.Ctor<ICacheRepository>("cache").Is(ctx => ctx.GetInstance<ICacheRepository>("Singleton"));
			For<UserAttributeValueManager>().Use<UserAttributeValueManager>();

			// authentication.
			For<AuthenticationProvider>().Use<AuthenticationProvider>().Ctor<int>("saltSize").Is(20);
			For<AuthenticationSessionProvider>().Use<AuthenticationSessionProvider>()
				.Ctor<ICacheRepository>("cacheRepository").Is(ctx => ctx.GetInstance<ICacheRepository>("Personal"));

			// userimages.
			For<UserImageManager>().Use<UserImageManager>();
			For<UserImageTransformer>().Use<UserImageTransformer>();
			For<UserImageDataManager>().Use<UserImageDataManager>()
				.Ctor<string>("basePath").Is(HttpContext.Current.Server.MapPath("~/App_Data"));

            For<SimpleSearchProvider>().Singleton().Use<SimpleSearchProvider>();
		}
	}
}
