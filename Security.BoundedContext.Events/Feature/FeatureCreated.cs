using Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Events
{
    public class FeatureCreated : EventBase
    {
        public string Name { get; private set; }
        public FeatureCreated(string name)
        {
            Name = name;
        }
    }
}
