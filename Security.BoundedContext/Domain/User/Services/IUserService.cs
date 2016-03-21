using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain.User.Services
{
    public interface IUserService : IDomainService
    {
        bool IsUserNameDuplicated(string userName);
    }
}
