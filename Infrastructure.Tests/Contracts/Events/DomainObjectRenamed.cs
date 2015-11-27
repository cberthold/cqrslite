using Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tests.Contracts.Events
{
    public class DomainObjectRenamed : EventBase<DomainObjectRenamed, TestDomainObject>
    {
        public string Name { get; protected set; }

        public DomainObjectRenamed(Guid id, string name) : base(id)
        {
            Name = name;
        }
    }
}
