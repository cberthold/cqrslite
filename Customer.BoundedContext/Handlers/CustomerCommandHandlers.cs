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
using Customer.SalesForceClient;
using Customer.BoundedContext.ReadModel;
using Customer.SalesForceClient.Enterprise;

namespace Customer.BoundedContext.Handlers
{
    public class CustomerCommandHandlers :
        ICommandHandler<CreateCustomer>,
        ICommandHandler<UpdateCustomer>,
        ICommandHandler<DeactivateCustomer>,
        ICommandHandler<ActivateCustomer>,
        ICommandHandler<SynchronizeToSalesForce>
    {
        private readonly ISession session;
        private readonly ICustomerReadModelFacade readFacade;

        public CustomerCommandHandlers(
            ISession session,
            ICustomerReadModelFacade readFacade)
        {
            this.session = session;
            this.readFacade = readFacade;
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
            customer.UpdateBillingAddress(command.BillingAddress);

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

        public void Handle(SynchronizeToSalesForce command)
        {
            const decimal MAX_ACCOUNTS_TO_SEND = 500m;

            using (var client = SalesForceServiceFactory.Create())
            {
                // use our read domain to get the accounts to link
                // in SalesForce.com
                var customerAccounts = from c in readFacade
                                        .GetCustomers()
                                       select new SystemAccount__c()
                                       {
                                           Name = (c.Name ?? "").Left(80),
                                           AccountName__c = c.Name,
                                           EnterpriseEntityId__c = c.Id.ToString("N")
                                       };

                var groupedAccounts = from c in customerAccounts
                                       .Select((c, i) => new { Customer = c, Index = (int)((decimal)i / MAX_ACCOUNTS_TO_SEND) })
                                      group c.Customer by c.Index;

                // loop through groups
                foreach (var group in groupedAccounts)
                {
                    if (group.Count() == 0) continue;

                    // cast them to sObject to pass to the service client
                    var sObjects = group.Cast<sObject>().ToArray();

                    // upsert the list
                    client.upsert("EnterpriseEntityId__c", sObjects);
                }
            }

        }

    }


}
