using Customer.BoundedContext.Commands;
using Customer.BoundedContext.Handlers;
using Infrastructure.Commands;
using Infrastructure.Domain;
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
        private static readonly InMemoryDomainRespository repository = new InMemoryDomainRespository();
        private CommandDispatcher dispatcher;
        private CustomerCommandHandlers handler;

        public CustomerController()
        {
            dispatcher = new CommandDispatcher(repository, null, null);
            handler = new CustomerCommandHandlers(repository);
            dispatcher.RegisterHandler<CreateCustomer>(handler);
            dispatcher.RegisterHandler<UpdateCustomer>(handler);
            dispatcher.RegisterHandler<DeactivateCustomer>(handler);
            dispatcher.RegisterHandler<ActivateCustomer>(handler);
        }


        // GET: api/Customer
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Customer/5
        public string Get(Guid id)
        {
            return "value";
        }

        // POST: api/Customer
        public void Post([FromBody]CreateCustomer command)
        {
            dispatcher.ExecuteCommand(command);
            Ok();
        }

        // PUT: api/Customer/5
        public void Put(Guid id, [FromBody]UpdateCustomer command)
        {
            dispatcher.ExecuteCommand(command);
            Ok();
        }

        // DELETE: api/Customer/5
        public void Delete(Guid id)
        {
            var command = new DeactivateCustomer()
            {
                Id = id
            };

            dispatcher.ExecuteCommand(command);

            Ok();

        }
    }
}
