using Customer.BoundedContext.ReadModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BoundedContext.ReadModel
{
    public interface ICustomerReadModelFacade
    {
        IEnumerable<CustomerListDTO> GetCustomers();
    }
}
