
using Customer.BoundedContext.Commands;
using Customer.BoundedContext.Handlers;
using Infrastructure.Commands;
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
        
        private IDomainRepository domainRepository;

        private CommandDispatcher dispatcher;
        private CustomerCommandHandlers handler;
        

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
              
            }

            base.Dispose(disposing);
        }

        public CustomerController()
        {
            //domainRepository = new InMemoryDomainRespository();
            domainRepository = new SqlDomainRepository("DefaultConnection");
            handler = new CustomerCommandHandlers(domainRepository);
            dispatcher = new CommandDispatcher();
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
