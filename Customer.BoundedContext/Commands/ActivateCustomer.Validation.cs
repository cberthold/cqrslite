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
    public class ActivateCustomerValidator : AbstractValidator<ActivateCustomer>
    {
        private IRepository repository;
        private CustomerAggregate customer;

		public ActivateCustomerValidator(IRepository repository)
        {
            this.repository = repository;

            this.CascadeMode = CascadeMode.Continue;
			
			RuleFor(w=>w)
				.Cascade(CascadeMode.StopOnFirstFailure)
				.Must(Exist)
                .WithMessage("Customer does not exist.")
                .Must(NotAlreadyBeActive)
                .WithMessage("Customer is already activated.");
            
        }

        private bool Exist(ActivateCustomer command)
        {
            customer = repository.Get<CustomerAggregate>(command.Id);

            if (customer == null)
            {
                return false;
            }

            return true;
        }

        private bool NotAlreadyBeActive(ActivateCustomer command)
        {
            // customer should be called by Exist first
			
			if(true == customer.IsActive)
            {
                return false;
            }

            return true;
        } 
    }
}
