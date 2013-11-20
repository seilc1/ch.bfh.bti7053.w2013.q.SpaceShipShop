using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StructureMap;
using Uniques.Library.Authentication;

namespace Uniques.Controllers.Api
{
    public class AuthenticateController : ApiController
    {
        private AuthenticationSessionProvider Provider
        {
            get { return ObjectFactory.GetInstance<AuthenticationSessionProvider>(); }
        }

        public bool Get()
        {
            return false;
        }

        public bool Post([FromBody]LoginModelWithEmail loginModel)
        {
            return Provider.Authenticate(loginModel);
        }

        public bool Post([FromBody]LoginModelWithUserName loginModel)
        {
            return Provider.Authenticate(loginModel);
        }

        public void Delete()
        {
            Provider.Destroy();
        }
    }
}
