using Infrastructure.Domain;
using Security.BoundedContext.Domain.Feature.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain.Feature.Services
{
    public interface IFeatureService : IDomainService
    {
        FeatureBookAggregate LoadFeature(Guid id);
    }
}
