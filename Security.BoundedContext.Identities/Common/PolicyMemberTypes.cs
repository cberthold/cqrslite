using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain.RootPolicy.Identities
{
    public enum PolicyMemberTypes
    {
        /// <summary>
        /// Root Policy - Top level policy defining features 
        /// available to the policies and children underneath
        /// with no access
        /// </summary>
        Root = 0,

        /// <summary>
        /// Root Child Policy - defines policies that override
        /// the root policy access to begin enabling features
        /// available to Child Root, Customer, and User Policies
        /// </summary>
        RootChild = 1,

        /// <summary>
        /// Customer Policy - defines policies that override
        /// the root and child policy access and is controlled
        /// at the customer level that users are associated with
        /// </summary>
        Customer = 2,

        /// <summary>
        /// User Policy - defines the policy available to the user
        /// </summary>
        User = 3,
    }
}
