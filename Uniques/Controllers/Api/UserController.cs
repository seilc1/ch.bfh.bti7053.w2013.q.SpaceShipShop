using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StructureMap;
using Uniques.Library.Authentication;
using Uniques.Library.Data;
using Uniques.Library.Users;

namespace Uniques.Controllers.Api
{
    public class UserController : ApiController
    {
        private UserManager UserManager
        {
            get { return ObjectFactory.GetInstance<UserManager>(); }
        }

        public User Get(int id)
        {
            return UserManager.Get(id);
        }

        public User Get(string displayname)
        {
            return UserManager.Get(displayname);
        }

        public User Put([FromBody]User user)
        {
            return UserManager.Add(user);
        }
    }
}
