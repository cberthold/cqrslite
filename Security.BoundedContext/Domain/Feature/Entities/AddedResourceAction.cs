using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain.Feature.Entities
{
    public class AddedResourceAction : ValueObject<AddedResourceAction>
    {
        public Guid ApiServiceId { get; private set; }
        public Guid ResourceActionEntityId { get; private set; }

        public AddedResourceAction(Guid apiServiceId, Guid resourceActionEntityId)
        {
            ApiServiceId = apiServiceId;
            ResourceActionEntityId = resourceActionEntityId;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return ApiServiceId;
            yield return ResourceActionEntityId;
        }
    }
}
