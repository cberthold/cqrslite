using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain
{
    public abstract class PolicyBase : IEntity
    {
        public Guid Id { get; private set; }

        protected PolicyBase(Guid id)
        {
            Id = id;
        }
    }
}
