using Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Events
{
    public class ApiServicesCreated : EventBase
    {
        public ApiServicesCreated(Guid id)
        {
            Id = id;
        }
    }
}
