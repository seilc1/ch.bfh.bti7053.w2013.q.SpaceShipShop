using System;
using System.Threading;

namespace Uniques.Library.Utilities
{
    public class ReaderLock : IDisposable
    {
        private readonly ReaderWriterLock _rwlLock;

        private readonly int _timeout;

        public ReaderLock(ReaderWriterLock rwlLock, int timeout)
        {
            _rwlLock = rwlLock;
            _timeout = timeout;
            _rwlLock.AcquireReaderLock(timeout);
        }

        public void UpgradeToWriterLock()
        {
            _rwlLock.UpgradeToWriterLock(_timeout);
        }

        public void Dispose()
        {
            _rwlLock.ReleaseReaderLock();
        }
    }
}
