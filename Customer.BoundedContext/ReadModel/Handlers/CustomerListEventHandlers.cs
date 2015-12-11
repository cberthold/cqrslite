using CQRSlite.Events;
using Customer.BoundedContext.Events;
using Customer.BoundedContext.ReadModel.DTO;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BoundedContext.ReadModel.Handlers
{
    public class CustomerListEventHandlers :
        IEventHandler<CustomerCreated>,
        IEventHandler<CustomerUpdated>,
        IEventHandler<CustomerDeactivated>,
        IEventHandler<CustomerActivated>
    {
        IReadRepository<CustomerListDTO> repository;

        public CustomerListEventHandlers(IReadRepository<CustomerListDTO> repository)
        {
            this.repository = repository;
        }

        public void Handle(CustomerDeactivated message)
        {
            var dto = repository.GetById(message.Id);

            dto.IsActive = false;
            dto.Modified = message.TimeStamp;

            repository.Update(dto);
        }

        public void Handle(CustomerActivated message)
        {
            var dto = repository.GetById(message.Id);

            dto.IsActive = true;
            dto.Modified = message.TimeStamp;

            repository.Update(dto);
        }

        public void Handle(CustomerUpdated message)
        {
            var dto = repository.GetById(message.Id);

            dto.Name = message.Name;
            dto.Modified = message.TimeStamp;

            repository.Update(dto);
        }

        public void Handle(CustomerCreated message)
        {
            var dto = new CustomerListDTO()
            {
                Id = message.Id,
                Name = message.Name,
                Created = message.TimeStamp,
                Modified = message.TimeStamp
            };

            repository.Insert(dto);
        }
    }
}
