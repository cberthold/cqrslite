using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Customer.API.Controllers
{
    public class AdminApiController : ApiController
    {
        // GET: api/Admin
        [HttpGet]
        [Route("api/AdminApi/RebuildSearchDb")]
        public object RebuildSearchDb()
        {
            dynamic obj = new ExpandoObject();
            obj.Result = true;

            return obj;
        }

        [HttpGet]
        [Route("api/AdminApi/RebuildReadDb")]
        public object RebuildReadDb()
        {
            dynamic obj = new ExpandoObject();
            obj.Result = true;

            return obj;
        }

    }
}
