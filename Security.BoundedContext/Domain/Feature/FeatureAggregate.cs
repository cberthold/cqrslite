using CQRSlite.Domain;
using Infrastructure.Domain;
using Infrastructure.Exceptions;
using Security.BoundedContext.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain
{
    public class FeatureAggregate : AggregateRoot
    {
        public string Name { get; protected set; }
        public bool IsEnabled { get; protected set; }

        protected List<ResourceActionEntity> resourceActions;
        public IList<ResourceActionEntity> ResourceActions
        {
            get { return resourceActions.AsReadOnly(); }
        }

        public void AddResourceAction(ResourceActionEntity resourceAction)
        {
            if(resourceActions.Any(a=> a.Id == resourceAction.Id))
                throw new DomainException("duplicate resource action");

            ApplyChange(new ResourceActionAddedToFeature());
            
        }
    }
}
