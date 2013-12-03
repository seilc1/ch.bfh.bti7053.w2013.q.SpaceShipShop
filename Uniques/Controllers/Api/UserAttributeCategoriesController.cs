using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StructureMap;
using Uniques.Library.Data;
using Uniques.Library.Mvc;
using Uniques.Library.Users.Attributes;

namespace Uniques.Controllers.Api
{
	public class UserAttributeCategoriesController : ApiController
	{
		private UserAttributeManager UserAttributeManager
		{
			get { return ObjectFactory.GetInstance<UserAttributeManager>(); }
		}

		public IEnumerable<UserAttributeCategory> Get()
		{
			return UserAttributeManager.UserAttributeCategories;
		}

		[RequiresRouteValues("categoryName")]
		public UserAttributeCategory Get(string categoryName)
		{
			return UserAttributeManager
				.UserAttributeCategories
				.FirstOrDefault(cat => cat.TextKey.Equals(categoryName, StringComparison.CurrentCultureIgnoreCase));
		}

		[RequiresRouteValues("categoryId")]
		public UserAttributeCategory Get(int categoryId)
		{
			return UserAttributeManager
				.UserAttributeCategories
				.FirstOrDefault(cat => cat.Id == categoryId);
		}

		public UserAttributeCategory Put(UserAttributeCategory category)
		{
			return UserAttributeManager.PutAttributeCategory(category);
		}

		public void Delete(UserAttributeCategory category)
		{
			UserAttributeManager.RemoveAttributeCategory(category);
		}
	}
}
