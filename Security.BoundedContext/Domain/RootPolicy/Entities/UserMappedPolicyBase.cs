using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain
{
    public class UserMappedPolicy<TType> : IEntity
        where TType : IIdentity
    {
        public Guid Id { get; private set; }
        public TType MappedIdentity { get; private set; }

        protected UserMappedPolicy(Guid id, TType mappedIdentity)
        {
            Id = id;
            MappedIdentity = mappedIdentity;
        }
    }
}
