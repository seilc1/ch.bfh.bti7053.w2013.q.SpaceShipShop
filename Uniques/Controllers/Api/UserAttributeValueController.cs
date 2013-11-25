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

        public IEnumerable<UserAttributeValue> Get(int id)
        {
            return AttributeValueManagerManager.GetValues(UserManager.Get(id));
        }
        
        public IEnumerable<UserAttributeValue> Get(string loginname)
        {
            return AttributeValueManagerManager.GetValues(UserManager.Get(loginname));
        }

        public UserAttributeValue Post(int id, UserAttributeValue value)
        {
            AttributeValueManagerManager.SetValue(value);
            return value;
        }

        public void Delete(int id, UserAttributeValue value)
        {
            AttributeValueManagerManager.DeleteValue(value);
        }

    }
}
