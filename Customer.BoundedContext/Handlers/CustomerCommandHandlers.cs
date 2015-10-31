using Customer.BoundedContext.Commands;
using Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonDomain;
using Infrastructure.Domain;
using Customer.BoundedContext.Domain;
using Infrastructure.Exceptions;

namespace Customer.BoundedContext.Handlers
{
    public class CustomerCommandHandlers :
        IHandle<CreateCustomer>,
        IHandle<UpdateCustomer>
    {
        private readonly IDomainRepository repository;

        public CustomerCommandHandlers(IDomainRepository repository)
        {
            this.repository = repository;
        }

        public IAggregate Handle(CreateCustomer command)
        {
            try
            {
                var customer = repository.GetById<CustomerAggregate>(command.Id);
                throw new AggregateAlreadyExistsException<CustomerAggregate>(command.Id);
            }
            catch (AggregateNotFoundException)
            {
                // We expect not to find anything
            }
            var newCustomer = CustomerAggregate.Create(command.Id, command.Name);
            
            if(command.BillingAddress != null)
            {
                newCustomer.UpdateBillingAddress(command.BillingAddress);
            }

            return newCustomer;
            
        }

        public IAggregate Handle(UpdateCustomer command)
        {
            var customer = repository.GetById<CustomerAggregate>(command.Id);
            customer.Update(command.Name);

            if(customer.BillingAddress != command.BillingAddress)
            {
                customer.UpdateBillingAddress(command.BillingAddress);
            }

            return customer;
        }

    }
}
