using Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BoundedContext.Commands
{
    public class DeactivateCustomer : ICommand<DeactivateCustomer>
    {
        public Guid Id { get; set; }
    }
}
