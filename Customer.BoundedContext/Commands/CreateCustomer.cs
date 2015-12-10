using CQRSlite.Commands;
using Customer.BoundedContext.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BoundedContext.Commands
{
    public class CreateCustomer : ICommand
    {
        public Guid Id { get; set; }
        public int ExpectedVersion { get; set; }
        public string Name { get; set; }
        public Address BillingAddress { get; set; }
    }
}
