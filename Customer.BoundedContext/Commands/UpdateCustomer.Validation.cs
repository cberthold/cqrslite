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
    public class UpdateCustomerValidator : AbstractValidator<UpdateCustomer>
    {
        private IRepository repository;
        private CustomerAggregate customer;

		public UpdateCustomerValidator(IRepository repository)
        {
            this.repository = repository;

            this.CascadeMode = CascadeMode.Continue;
			
			RuleFor(w=>w)
				.Cascade(CascadeMode.StopOnFirstFailure)
				.Must(Exist)
                .WithMessage("Customer does not exist.")
                .Must(NotAlreadyBeInactive)
                .WithMessage("Customer is already Deactivated.");

            RuleFor(w => w.Name)
                .NotNull()
                .Length(10, 150);

            RuleFor(w => w.BillingAddress.State)
                .NotEqual("FL")
                .WithMessage("Too many flip flops");
            
        }

        private bool Exist(UpdateCustomer command)
        {
            customer = repository.Get<CustomerAggregate>(command.Id);

            if (customer == null)
            {
                return false;
            }

            return true;
        }

        private bool NotAlreadyBeInactive(UpdateCustomer command)
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
