using System.Collections.Generic;
using System.Threading;

namespace Uniques.Library.Caching
{
    public class ThreadCacheRepository : ICacheRepository
    {
        private const int LockTimeOut = 250;

        private static readonly Dictionary<string, object> Cache = new Dictionary<string, object>();

        private static readonly ReaderWriterLock Lock = new ReaderWriterLock();

        public object this[string name]
        {
            get
            {
                using (new ReaderLock(Lock, LockTimeOut))
                {
                    object result;
                    if (Cache.TryGetValue(name, out result))
                    {
                        return result;
                    }

                    return null;
                }
            }

            set
            {
                using (new WriterLock(Lock, LockTimeOut))
                {
                    if (Cache.ContainsKey(name))
                    {
                        Cache[name] = value;
                    }
                    else
                    {
                        Cache.Add(name, value);
                    }
                }
            }
        }

        public void Delete(string name)
        {
            using (new WriterLock(Lock, LockTimeOut))
            {
                Cache.Remove(name);
            }
        }
    }
}
