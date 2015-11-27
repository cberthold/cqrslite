using Customer.BoundedContext.ValueObjects;
using Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BoundedContext.Commands
{
    public class UpdateCustomer : ICommand<UpdateCustomer>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Address BillingAddress { get; set; }
    }
}
