using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StructureMap;
using Uniques.Library.Data;
using Uniques.Library.Mvc;
using Uniques.Library.Users.SimpleSearch;

namespace Uniques.Controllers.Api
{
    public class SearchController : ApiController
    {
        private SimpleSearchProvider SearchProvider
        {
            get { return ObjectFactory.GetInstance<SimpleSearchProvider>(); }
        }

        [RequiresRouteValues("searchTerm")]
        public IEnumerable<MinimalUser> Get(string searchTerm)
        {
            return SearchProvider.Search(searchTerm);
        }
    }
}
