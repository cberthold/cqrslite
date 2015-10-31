using Customer.BoundedContext.Commands;
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

        }

        // PUT: api/Customer/5
        public void Put(Guid id, [FromBody]UpdateCustomer command)
        {
        }

        // DELETE: api/Customer/5
        public void Delete(Guid id)
        {

        }
    }
}
