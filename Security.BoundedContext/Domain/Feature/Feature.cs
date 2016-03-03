using CQRSlite.Domain;
using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain
{
    public class Feature : AggregateRoot
    {
        public string Name { get; protected set; }
        public bool IsEnabled { get; protected set; }

        protected List<ResourceActionEntity> resourceActions;
        public IList<ResourceActionEntity> ResourceActions
        {
            get { return resourceActions.AsReadOnly(); }
        }
    }
}
