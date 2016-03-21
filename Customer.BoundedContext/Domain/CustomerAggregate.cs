
using CQRSlite.Domain;
using Customer.BoundedContext.Events;
using Customer.BoundedContext.Identities;
using Customer.BoundedContext.ValueObjects;
using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Customer.BoundedContext.Domain
{
    public class CustomerAggregate : AggregateRoot
    {

        #region Identity

        public override Guid Id
        {
            get { return CustomerId.Value; }
            protected set
            {
                CustomerId = new CustomerId(value);
            }
        }

        public CustomerId CustomerId { get; private set; }

        #endregion

        #region State

        public string Name { get; protected set; }
        public Address BillingAddress { get; protected set; }
        public bool IsActive { get; protected set; }

        #endregion

        #region Apply State

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

        #region Private Constructor

        private CustomerAggregate() { }

        #endregion

        #region Aggregate Creation

        protected CustomerAggregate(CustomerId customerId, string name)
        {
            ApplyChange(new CustomerCreated(customerId, name));
            ApplyChange(new CustomerActivated(customerId));
        }

        internal static CustomerAggregate Create(Guid id, string name)
        {
            var newId = new CustomerId(id);
            return new CustomerAggregate(
                newId,
                name
                );

        }

        #endregion

        #region Domain methods

        internal void Activate()
        {
            ApplyChange(new CustomerActivated(CustomerId));
        }

        internal void Deactivate()
        {
            ApplyChange(new CustomerDeactivated(CustomerId));
        }


        internal void UpdateBillingAddress(Address billingAddress)
        {
            if (!object.Equals(this.BillingAddress, billingAddress))
            {
                ApplyChange(new CustomerBillingAddressUpdated(CustomerId, billingAddress));
            }
        }

        internal void Update(string name)
        {
            ApplyChange(new CustomerUpdated(CustomerId, name));
        }

        #endregion
    }
}
