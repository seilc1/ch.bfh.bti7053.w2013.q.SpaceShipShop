using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StructureMap;
using Uniques.Library.Common;
using Uniques.Library.Data;
using Uniques.Library.Mvc;
using Uniques.Library.Users.Attributes;

namespace Uniques.Controllers.Api
{
	public class UserAttributesController : ApiController
	{
		private UserAttributeManager UserAttributeManager
		{
			get { return ObjectFactory.GetInstance<UserAttributeManager>(); }
		}

		public IEnumerable<UserAttribute> Get()
		{
			return UserAttributeManager.UserAttributes;
		}

		[HttpGet]
		[RequiresRouteValues("categoryName")]
		public IEnumerable<UserAttribute> GetByCategory(string categoryName)
		{
			return UserAttributeManager.GetAttributesByCategory(categoryName);
		}

		[RequiresRouteValues("attributeId")]
		public UserAttribute Get(int attributeId)
		{
			return UserAttributeManager.GetAttribute(attributeId);
		}

		[RequiresRouteValues("attributeName")]
		public UserAttribute Get(string attributeName)
		{
			return UserAttributeManager.GetAttribute(attributeName);
		}

		public UserAttribute Put(UserAttribute userAttribute)
		{
			UserAttributeManager.PutAttribute(userAttribute);
			return userAttribute;
		}

		public void Delete(UserAttribute userAttribute)
		{
			UserAttributeManager.RemoveAttribute(userAttribute);
		}
	}
}
