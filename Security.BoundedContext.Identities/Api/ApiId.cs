using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Identities.Api
{
    public class ApiId
    {
        public Guid Value { get; private set; }

        public ApiId(Guid id)
        {
            Value = id;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is ApiId)) return false;

            return Value == ((ApiId)obj).Value;
        }
    }
}
