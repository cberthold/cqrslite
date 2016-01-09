using CQRSlite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Domain
{
    public interface ISessionHook : IDisposable
    {
        void AddAggregate<TAggregate>(TAggregate aggregate) where TAggregate : AggregateRoot;
        bool PreCommit(IEnumerable<HookableSession.AggregateDescriptor> trackedAggregates);
        void PostCommit();

    }
}
