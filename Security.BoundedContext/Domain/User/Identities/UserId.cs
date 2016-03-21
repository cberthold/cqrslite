using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain.User.Identities
{
    public class UserId
    {
        public Guid Value { get; private set; }

        public UserId(Guid id)
        {
            Value = id;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is UserId)) return false;

            return Value == ((UserId)obj).Value;
        }
    }
}
