using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using StructureMap;
using Uniques.Library.Authentication;
using Uniques.Library.Data;
using Uniques.Library.Mvc;
using Uniques.Library.Users;

namespace Uniques.Controllers.Api
{
	public class UserController : ApiController
	{
		private UserManager UserManager
		{
			get { return ObjectFactory.GetInstance<UserManager>(); }
		}

        public HttpResponseMessage Get()
        {
            return this.Request.CreateResponse(
                HttpStatusCode.OK,
                new 
                    {
                        Desc = "Get specific User by 'Loginname' or 'UserId' or search for an User by a value with 'where(value)'",
                        Users = UserManager.All
                    });
        }

		[RequiresRouteValues("userId")]
		public MinimalUser Get(int userId)
		{
			return UserManager.Get(userId);
		}

		[RequiresRouteValues("loginname")]
        public MinimalUser Get(string loginname)
		{
			return UserManager.Get(loginname);
		}

		public User Put([FromBody]User user)
		{
			return UserManager.Add(user);
		}

		[System.Web.Http.Authorize]
		public void Delete()
		{
			var uniquesDataContext = ObjectFactory.GetInstance<UniquesDataContext>();

			foreach (var ele in uniquesDataContext.Users.ToList())
			{
				uniquesDataContext.Users.Remove(ele);
			}
		}
	}
}
