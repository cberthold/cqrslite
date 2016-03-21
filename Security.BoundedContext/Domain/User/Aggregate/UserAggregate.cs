using CQRSlite.Domain;
using Infrastructure.Exceptions;
using Security.BoundedContext.Domain.RootPolicy.Aggregate;
using Security.BoundedContext.Domain.RootPolicy.Services;
using Security.BoundedContext.Domain.User.Identities;
using Security.BoundedContext.Events.User;
using Security.BoundedContext.ReadModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain.User.Aggregate
{
    public class UserAggregate : AggregateRoot
    {

        #region Identities

        public override Guid Id
        {
            get { return UserId.Value; }
            protected set { UserId = new UserId(value); }
        }

        public UserId UserId { get; private set; }


        #endregion

        #region State

        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string UserName { get; protected set; }

        #endregion

        #region constructors

        private UserAggregate() {
        }

        protected UserAggregate(UserId userId, string userName, string firstName, string lastName)
            : this()
        {
            UserId = userId;
            ApplyChange(new UserProvisioned(userName, firstName, lastName));
        }

        #endregion

        #region domain methods

        public static UserAggregate Provision(string userName, string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new DomainException($"{nameof(userName)} can not be null or empty.");
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainException($"{nameof(firstName)} can not be null or empty.");
            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomainException($"{nameof(lastName)} can not be null or empty.");
            var id = new UserId(Guid.NewGuid());
            var aggregate = new UserAggregate(id, userName, firstName, lastName);

            return aggregate;   
        }

        #endregion

        #region apply state

        public void Apply(UserProvisioned @event)
        {
            FirstName = @event.FirstName;
            LastName = @event.LastName;
            UserName = @event.UserName;
        }

        #endregion
    }

    
}
