﻿using Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tests.Contracts.Events
{
    public class DomainObjectCreated : EventBase<DomainObjectCreated, TestDomainObject>
    {
        public string Name { get; protected set; }

        public DomainObjectCreated(Guid id, string name) : base(id)
        {
            Name = name;
        }
    }
}
