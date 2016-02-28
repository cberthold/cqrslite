using CQRSlite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain
{
    public class UserAggregate : AggregateRoot
    {
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string UserName { get; protected set; }

        private UserAggregate()
        {

        }
    }
}
