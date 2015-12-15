using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Customer.BoundedContext.ReadModel.DTO;
using Infrastructure.Repository;
using CQRSlite.Domain;
using Customer.BoundedContext.Domain;

namespace Customer.BoundedContext.ReadModel
{
    public class CustomerReadModelFacade : ICustomerReadModelFacade
    {
        IRepository writeRepository;
        IReadRepository<CustomerListDTO> customerListRepository;

        public CustomerReadModelFacade(IRepository writeRepository, IReadRepository<CustomerListDTO> customerListRepository)
        {
            this.writeRepository = writeRepository;
            this.customerListRepository = customerListRepository;
        }

        public CustomerAggregate Get(Guid id)
        {
            return writeRepository.Get<CustomerAggregate>(id);
        }

        public IEnumerable<CustomerListDTO> GetCustomers()
        {
            return customerListRepository.GetCollection().ToList();
        }

    }
}
