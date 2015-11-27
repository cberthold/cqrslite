using Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BoundedContext.Commands
{
    public class ActivateCustomer : ICommand<ActivateCustomer>
    {
        public Guid Id { get; set; }
    }
}
