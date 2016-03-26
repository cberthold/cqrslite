using Infrastructure.Events;
using Security.BoundedContext.Identities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Events.User
{
    public class UserProvisioned : EventBase
    {
        public UserId UserId { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string UserName { get; protected set; }
        public UserProvisioned(UserId userId, string userName, string firstName, string lastName)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
        }
    }
}
