using CQRSlite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain.RootPolicy.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IRepository repository;
        public PolicyService(IRepository repository)
        {
            this.repository = repository;
        }

        public TRoot LoadRootPolicy<TRoot>(Guid id)
            where TRoot : RootPolicyAggregate<TRoot>
        {
            return repository.Get<TRoot>(id);
        }
    }
}
