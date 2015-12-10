using CQRSlite.Domain;
using CQRSlite.Events;
using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Events
{
    public interface IEvent<TAggregate> : IEvent
        where TAggregate : AggregateRoot
    {

    }
}
