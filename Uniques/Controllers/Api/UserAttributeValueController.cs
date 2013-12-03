using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StructureMap;
using Uniques.Library.Data;
using Uniques.Library.Mvc;
using Uniques.Library.Users;
using Uniques.Library.Users.Attributes;

namespace Uniques.Controllers.Api
{
	public class UserAttributeValueController : ApiController
	{
		private UserAttributeValueManager AttributeValueManagerManager
		{
			get { return ObjectFactory.GetInstance<UserAttributeValueManager>(); }
		}

		private UserManager UserManager
		{
			get { return ObjectFactory.GetInstance<UserManager>(); }
		}

		[RequiresRouteValues("userId")]
		public IEnumerable<UserAttributeValueSet> Get(int userId)
		{
			return AttributeValueManagerManager.GetValues(UserManager.Get(userId));
		}

		[RequiresRouteValues("loginname")]
		public IEnumerable<UserAttributeValueSet> Get(string loginname)
		{
			return AttributeValueManagerManager.GetValues(UserManager.Get(loginname));
		}

		[RequiresRouteValues("loginname,categoryName")]
		public IEnumerable<UserAttributeValueSet> Get(string loginname, string categoryName)
		{
			return AttributeValueManagerManager.GetValues(UserManager.Get(loginname), categoryName);
		}

		[RequiresRouteValues("userId,categoryName")]
		public IEnumerable<UserAttributeValueSet> Get(int userId, string categoryName)
		{
			return AttributeValueManagerManager.GetValues(UserManager.Get(userId), categoryName);
		}

		public UserAttributeValueSet Post(int userId, UserAttributeValueSet value)
		{
			AttributeValueManagerManager.SetValue(userId, value);
			return value;
		}

		public void Delete(int userId, UserAttributeValueSet value)
		{
			AttributeValueManagerManager.DeleteValue(value);
		}
	}
}
