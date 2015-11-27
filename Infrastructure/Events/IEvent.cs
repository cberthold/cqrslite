using Infrastructure.Domain;
using Infrastructure.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Events
{
    public interface IEvent<TAggregate> : IEvent
        where TAggregate : IAggregate<TAggregate>
    {

    }
    public interface IEvent : IMessage
    {
        Guid Id { get; }
    }
}
