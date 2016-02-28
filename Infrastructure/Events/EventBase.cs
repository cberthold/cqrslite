using CQRSlite.Domain;
using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CQRSlite.Events;

namespace Infrastructure.Events
{
    public abstract class EventBase : IEvent
    {
        public Guid Id { get; set; }

        public DateTimeOffset TimeStamp { get; set; }

        public int Version { get; set; }


        public EventBase(Guid id)
        {
            Id = id;

        }

        protected EventBase() { }
    }


}
