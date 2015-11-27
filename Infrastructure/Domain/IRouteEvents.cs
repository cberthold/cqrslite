using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Domain
{
    public interface IRouteEvents<TAggregate>
        where TAggregate : AggregateBase<TAggregate>
    {
        void Register<T>(Action<T> handler);

        void Register(IAggregate<TAggregate> aggregate);

        void Dispatch(object eventMessage);
    }
}
