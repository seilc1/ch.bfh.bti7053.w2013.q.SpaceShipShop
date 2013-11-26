using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StructureMap;
using Uniques.Library.Data;
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

        public IEnumerable<UserAttributeValueSet> Get(int id)
        {
            return AttributeValueManagerManager.GetValues(UserManager.Get(id));
        }

        public IEnumerable<UserAttributeValueSet> Get(string loginname)
        {
            return AttributeValueManagerManager.GetValues(UserManager.Get(loginname));
        }

        public UserAttributeValueSet Post(int id, UserAttributeValueSet value)
        {
            AttributeValueManagerManager.SetValue(id, value);
            return value;
        }

        public void Delete(int id, UserAttributeValueSet value)
        {
            AttributeValueManagerManager.DeleteValue(value);
        }

    }
}
