using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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

        [RequiresRouteValues("userId")]
        public User Get(int userId)
        {
            return UserManager.Get(userId);
        }

        [RequiresRouteValues("loginname")]
        public User Get(string loginname)
        {
            return UserManager.Get(loginname);
        }

        public User Put([FromBody]User user)
        {
            return UserManager.Add(user);
        }

        [Authorize]
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
