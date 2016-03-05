using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain.RootPolicy.Aggregate
{
    public interface IPolicy
    {
        Guid Id { get; }
        Guid? ParentPolicyId { get; }
        bool IsRootPolicy { get; }
    }
}
