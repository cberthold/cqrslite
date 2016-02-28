using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BoundedContext.ValueObjects
{
    public class Address : ValueObject<Address>
    {

        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Zipcode { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Address1;
            yield return Address2;
            yield return City;
            yield return State;
            yield return Zipcode;
        }
    }
}
