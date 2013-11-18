namespace Uniques.Library.Caching
{
    public class HttpContextCacheRepository : ICacheRepository
    {
        private Cache Cache
        {
            get { return HttpContext.Current.Cache; }
        }

        public object this[string name]
        {
            get { return Cache[name];  }
            set { Cache[name] = value; }
        }

        public void Delete(string name)
        {
            Cache.Remove(name);
        }
    }
}
