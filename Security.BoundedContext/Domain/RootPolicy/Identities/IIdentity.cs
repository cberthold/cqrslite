using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain
{
    public interface IIdentity { }
    public interface IIdentity<out TType> : IIdentity
    {
        TType IdValue { get; }
        string Name { get; }
    }
}
