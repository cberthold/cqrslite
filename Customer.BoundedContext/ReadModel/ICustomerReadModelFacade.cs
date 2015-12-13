using Customer.BoundedContext.Domain;
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
        CustomerAggregate Get(Guid id);
        IEnumerable<CustomerListDTO> GetCustomers();
    }
}
