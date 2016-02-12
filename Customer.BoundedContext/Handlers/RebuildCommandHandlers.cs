using CQRSlite.Commands;
using CQRSlite.Domain;
using Customer.BoundedContext.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BoundedContext.Handlers
{
    public class RebuildCommandHandlers
        : ICommandHandler<RebuildReadDbCommand>
    {
        private readonly ISession session;

        public RebuildCommandHandlers(ISession session)
        {
            this.session = session;
        }

        public void Handle(RebuildReadDbCommand message)
        {
            
        }
    }
}
