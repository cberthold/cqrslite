using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Identities.Api
{
    public class ResourceActionId
    {
        public Guid ApiIdValue { get; private set; }
        public Guid Value { get; private set; }

        public ResourceActionId(ApiId apiId, Guid id)
        {
            ApiIdValue = apiId.Value;
            Value = id;
        }

        public override int GetHashCode()
        {
            int hash = 269;

            hash = (47 * hash) + Value.GetHashCode();
            hash = (47 * hash) + ApiIdValue.GetHashCode();

            return hash;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is ResourceActionId)) return false;

            var resourceActionId = (ResourceActionId)obj;

            return
                ApiIdValue == resourceActionId.ApiIdValue &&
                Value == resourceActionId.Value;
        }
    }
}
