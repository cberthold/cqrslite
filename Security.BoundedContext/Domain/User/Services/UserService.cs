using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain.User.Services
{
    public class UserService : IUserService
    {
        public bool IsUserNameDuplicated(string userName)
        {
            // TODO: Use read model to check duplicates
            return false;
        }
    }
}
