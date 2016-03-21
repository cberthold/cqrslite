using Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Events.User
{
    public class UserProvisioned : EventBase
    {
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string UserName { get; protected set; }
        public UserProvisioned(string userName, string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
        }
    }
}
