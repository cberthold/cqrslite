using Customer.BoundedContext.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Infrastructure.Domain;
using Customer.BoundedContext.Domain;
using CQRSlite.Domain.Exception;
using CQRSlite.Commands;
using CQRSlite.Domain;
using Infrastructure.Exceptions;

namespace Customer.BoundedContext.Handlers
{
    public class CustomerCommandHandlers :
        ICommandHandler<CreateCustomer>,
        ICommandHandler<UpdateCustomer>,
        ICommandHandler<DeactivateCustomer>,
        ICommandHandler<ActivateCustomer>
    {
        private readonly ISession session;

        public CustomerCommandHandlers(ISession session)
        {
            this.session = session;
        }

        public void Handle(CreateCustomer command)
        {
            try
            {
                var customer = session.Get<CustomerAggregate>(command.Id);
                throw new AggregateAlreadyExistsException<CustomerAggregate>(command.Id);
            }
            catch (AggregateNotFoundException)
            {
                // We expect not to find anything
            }
            var newCustomer = CustomerAggregate.Create(command.Id, command.Name);

            if (command.BillingAddress != null)
            {
                newCustomer.UpdateBillingAddress(command.BillingAddress);
            }
            session.Add(newCustomer);
            session.Commit();
        }

        public void Handle(UpdateCustomer command)
        {
            var customer = session.Get<CustomerAggregate>(command.Id);
            customer.Update(command.Name);

            if (customer.BillingAddress != command.BillingAddress)
            {
                customer.UpdateBillingAddress(command.BillingAddress);
            }

            session.Add(customer);
            session.Commit();

        }

        public void Handle(DeactivateCustomer command)
        {
            var customer = session.Get<CustomerAggregate>(command.Id);
            customer.Deactivate();
            session.Add(customer);
            session.Commit();
        }

        public void Handle(ActivateCustomer command)
        {
            var customer = session.Get<CustomerAggregate>(command.Id);
            customer.Activate();
            session.Add(customer);
            session.Commit();
        }

    }

   
}
