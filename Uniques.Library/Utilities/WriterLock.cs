using System;
using System.Threading;

namespace Uniques.Library.Utilities
{
    public class WriterLock : IDisposable
    {
        private readonly ReaderWriterLock _rwlLock;

        public WriterLock(ReaderWriterLock rwlLock, int timeout)
        {
            _rwlLock = rwlLock;
            _rwlLock.AcquireReaderLock(timeout);
        }

        public void Dispose()
        {
            _rwlLock.ReleaseReaderLock();
        }
    }
}
