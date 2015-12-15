using CQRSlite.Domain;
using Customer.BoundedContext.Domain;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BoundedContext.Commands.Validation
{
    public class DeactivateCustomerValidator : AbstractValidator<DeactivateCustomer>
    {
        private IRepository repository;
        private CustomerAggregate customer;

		public DeactivateCustomerValidator(IRepository repository)
        {
            this.repository = repository;

            this.CascadeMode = CascadeMode.Continue;
			
			RuleFor(w=>w)
				.Cascade(CascadeMode.StopOnFirstFailure)
				.Must(Exist)
                .WithMessage("Customer does not exist.");

            RuleFor(w => w)
                .Must(NotAlreadyBeInactive)
                .WithMessage("Customer is already Deactivated.");
            
        }

        private bool Exist(DeactivateCustomer command)
        {
            customer = repository.Get<CustomerAggregate>(command.Id);

            if (customer == null)
            {
                return false;
            }

            return true;
        }

        private bool NotAlreadyBeInactive(DeactivateCustomer command)
        {
            // customer should be called by Exist first
			
			if(false == customer.IsActive)
            {
                return false;
            }

            return true;
        } 
    }
}
