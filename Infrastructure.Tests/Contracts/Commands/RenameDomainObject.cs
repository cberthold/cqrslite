using Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tests.Contracts.Commands
{
    public class RenameDomainObject : ICommand<RenameDomainObject>
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }

        public RenameDomainObject(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
