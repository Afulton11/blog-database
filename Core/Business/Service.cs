using Core.Business.Contracts;
using System;
namespace Core.Business
{
    public abstract class Service : IService
    {

        #region IDisposable Support
        private bool isDisposed = false; // To detect redundant calls

        protected virtual void FreeManagedResources() {}
        protected virtual void FreeUnmanagedResources() {}

        private void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    FreeManagedResources();
                }

                FreeUnmanagedResources();

                isDisposed = true;
            }
        }

        ~Service()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
