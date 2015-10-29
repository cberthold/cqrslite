using CommonDomain;
using CommonDomain.Core;
using Infrastructure.Domain;
using Infrastructure.Tests.Contracts.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tests
{
    public class TestDomainObject : AggregateBase
    {

        public string Name { get; protected set; }

        public TestDomainObject()
        {
            Register<DomainObjectCreated>(Apply);
            Register<DomainObjectRenamed>(Apply);
        }

        private void Apply(DomainObjectRenamed obj)
        {
            Name = obj.Name;
        }

        private void Apply(DomainObjectCreated obj)
        {
            Id = obj.Id;
            Name = obj.Name;
        }

        private TestDomainObject(Guid id, string name) : this()
        {
            RaiseEvent(new DomainObjectCreated(id, name));
        }

        internal void Rename(string name)
        {
            RaiseEvent(new DomainObjectRenamed(Id, name));
        }

        internal static IAggregate Create(Guid id, string name)
        {
            return new TestDomainObject(id, name);
        }
    }
}
