using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain
{
    public class Identity<TType> : IIdentity<TType>
    {
        public TType IdValue { get; private set; }
        public string Name { get; private set; }

        public Identity(TType idValue, string name)
        {
            IdValue = idValue;
            Name = name;
        }
    }
}
