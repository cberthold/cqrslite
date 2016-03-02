using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.ValueObjects
{
    public class ApiServiceListValueObject : ValueObject<ApiServiceListValueObject>
    {
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; }
        
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return ServiceId;
            yield return ServiceName;
        }
    }
}
