using Infrastructure.Events;
using Security.BoundedContext.Identities.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Events
{
    public class ApiServiceCreated : EventBase
    {
        public ApiId ApiId { get; protected set; }
        public string Name { get; protected set; }
        public ApiServiceCreated(ApiId apiId, string name)
        {
            ApiId = apiId;
            Name = name;
        }
    }
}
