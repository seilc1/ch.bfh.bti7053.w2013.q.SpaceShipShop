using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.SessionState;
using StructureMap;
using Uniques.Library.Authentication;

namespace Uniques.Controllers.Api
{
    public class AuthenticateController : ApiController, IRequiresSessionState
    {
        private AuthenticationSessionProvider Provider
        {
            get { return ObjectFactory.GetInstance<AuthenticationSessionProvider>(); }
        }

        public bool Get()
        {
            return Provider.Authenticated;
        }

        public bool Post([FromBody]LoginModel loginModel)
        {
            return Provider.Authenticate(loginModel);
        }

        public void Delete()
        {
            Provider.Destroy();
        }
    }
}
