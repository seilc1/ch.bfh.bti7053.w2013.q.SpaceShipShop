using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using StructureMap;
using Uniques.Library.Data;
using Uniques.Library.Users;

namespace Uniques.Library
{
	public class UniquesContext
	{
		private const string CacheKey = "__Context";
		private bool? _loggedIn;
        private MinimalUser _currentUser;

		public static UniquesContext Current
		{
			get
			{
				return HttpContext.Current.Items[CacheKey] as UniquesContext
						?? (HttpContext.Current.Items[CacheKey] = new UniquesContext()) as UniquesContext;
			}
		}

		public bool Authenticated
		{
			get { return _loggedIn.HasValue ? _loggedIn.Value : (_loggedIn = HttpContext.Current.User.Identity.IsAuthenticated).Value; }
		}

		public MinimalUser User
		{
			get { return _currentUser ?? (_currentUser = UserManager.Get(HttpContext.Current.User.Identity.Name)); }
		}

		public UserManager UserManager
		{
			get
			{
				return ObjectFactory.GetInstance<UserManager>();
			}
		}
	}
}
