using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.BoundedContext.Domain.Feature.Aggregate;
using CQRSlite.Domain;

namespace Security.BoundedContext.Domain.Feature.Services
{
    public class FeatureService : IFeatureService
    {
        private readonly IRepository repository;

        public FeatureService(IRepository repository)
        {
            this.repository = repository;
        }
        public FeatureBookAggregate LoadFeature(Guid id)
        {
            var aggregate = repository.Get<FeatureBookAggregate>(id);

            return aggregate;
        }
    }
}
