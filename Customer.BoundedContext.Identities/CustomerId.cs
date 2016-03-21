using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BoundedContext.Identities
{
    public class CustomerId
    {
        public Guid Value { get; private set; }

        public CustomerId(Guid id)
        {
            Value = id;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is CustomerId)) return false;

            return Value == ((CustomerId)obj).Value;
        }
    }
}
