﻿
using CQRSlite.Bus;
using CQRSlite.Commands;
using CQRSlite.Domain;
using Customer.BoundedContext.Commands;
using Customer.BoundedContext.Domain;
using Customer.BoundedContext.Handlers;
using Customer.BoundedContext.ReadModel;
using Customer.BoundedContext.ReadModel.DTO;
using Infrastructure.Domain;
using Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Customer.API.Controllers
{
    public class CustomerController : ApiController
    {
        
        
        private ICommandSender dispatcher;
        private ICustomerReadModelFacade readModel;
        private IRepository writeRepository;
        
        
        public CustomerController(ICommandSender dispatcher, ICustomerReadModelFacade readModel, IRepository writeRepository)
        {
            this.dispatcher = dispatcher;
            this.readModel = readModel;
            this.writeRepository = writeRepository;
        }


        // GET: api/Customer
        public IEnumerable<CustomerListDTO> Get()
        {
            return readModel.GetCustomers();
        }

        // GET: api/Customer/5
        public CustomerAggregate Get(Guid id)
        {
            return writeRepository.Get<CustomerAggregate>(id);
        }

        // POST: api/Customer
        public void Post([FromBody]CreateCustomer command)
        {
            dispatcher.Send(command);
            Ok();
        }

        // PUT: api/Customer/5
        public void Put(Guid id, [FromBody]UpdateCustomer command)
        {
            dispatcher.Send(command);
            Ok();
        }

        // DELETE: api/Customer/5
        public void Delete(Guid id)
        {
            var command = new DeactivateCustomer()
            {
                Id = id
            };

            dispatcher.Send(command);

            Ok();

        }
    }
}
