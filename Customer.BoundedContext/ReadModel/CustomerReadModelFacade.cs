using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Customer.BoundedContext.ReadModel.DTO;
using Infrastructure.Repository;

namespace Customer.BoundedContext.ReadModel
{
    public class CustomerReadModelFacade : ICustomerReadModelFacade
    {
        IReadRepository<CustomerListDTO> customerListRepository;

        public CustomerReadModelFacade(IReadRepository<CustomerListDTO> customerListRepository)
        {
            this.customerListRepository = customerListRepository;
        }

        public IEnumerable<CustomerListDTO> GetCustomers()
        {
            return customerListRepository.GetCollection().ToList();
        }
    }
}
