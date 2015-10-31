using CommonDomain.Core;
using Customer.BoundedContext.Events;
using Customer.BoundedContext.ValueObjects;
using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonDomain;

namespace Customer.BoundedContext.Domain
{
    public class CustomerAggregate : AggregateBase
    {
        #region Domain Properties

        public string Name { get; protected set; }
        public Address BillingAddress { get; protected set; }
        public bool IsActive { get; protected set; }

        #endregion

        #region Constructor and Event Registration

        public CustomerAggregate()
        {
            Register<CustomerCreated>(Apply);
            Register<CustomerUpdated>(Apply);
            Register<CustomerBillingAddressUpdated>(Apply);
            Register<CustomerActivated>(Apply);
            Register<CustomerDeactivated>(Apply);
        }

        #endregion

        #region Apply Events to Domain

        private void Apply(CustomerCreated @event)
        {
            Id = @event.Id;
            Name = @event.Name;
        }

        private void Apply(CustomerUpdated @event)
        {
            Name = @event.Name;
        }

        private void Apply(CustomerBillingAddressUpdated @event)
        {
            var address = @event.BillingAddress;

            BillingAddress = new Address()
            {
                Address1 = address.Address1,
                Address2 = address.Address2,
                City = address.City,
                State = address.State,
                Zipcode = address.Zipcode
            };

        }

        private void Apply(CustomerActivated @event)
        {
            IsActive = true;
        }

        private void Apply(CustomerDeactivated @event)
        {
            IsActive = false;
        }

        #endregion

        #region Aggregate Creation

        private CustomerAggregate(Guid id, string name) : this()
        {
            RaiseEvent(new CustomerCreated()
            {
                Id = id,
                Name = name
            });
        }

        internal static CustomerAggregate Create(Guid id, string name)
        {
            return new CustomerAggregate(
                id,
                name
                );

        }

        #endregion

        #region Domain methods

        internal void UpdateBillingAddress(Address billingAddress)
        {
            RaiseEvent(new CustomerBillingAddressUpdated()
            {
                Id = this.Id,
                BillingAddress = billingAddress
            });

        }

        internal void Update(string name)
        {
            Name = name;
        }

        #endregion
    }
}
