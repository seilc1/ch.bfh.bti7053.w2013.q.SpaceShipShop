using System.Web;
using System.Web.SessionState;

namespace Uniques.Library.Caching
{
    public class SessionCacheRepository : ICacheRepository
    {
        private HttpSessionState Cache { get { return HttpContext.Current.Session; } }

        public object this[string name]
        {
            get { return Cache[name]; }
            set { Cache[name] = value; }
        }

        public void Delete(string name)
        {
            Cache.Remove(name);
        }
    }
}
