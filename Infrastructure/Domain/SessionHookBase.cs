using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRSlite.Domain;

namespace Infrastructure.Domain
{
    public class SessionHookBase : ISessionHook
    {
        public void AddAggregate<TAggregate>(TAggregate aggregate) where TAggregate : AggregateRoot
        {
            
        }

        public void PostCommit()
        {
           
        }

        public bool PreCommit(IEnumerable<HookableSession.AggregateDescriptor> trackedAggregates)
        {
            return true;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void DisposeManagedObjects()
        { }

        protected virtual void DisposeUnmanagedObjects()
        { }

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DisposeManagedObjects();
                }

                DisposeUnmanagedObjects();

                disposedValue = true;
            }
        }

        ~SessionHookBase()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
