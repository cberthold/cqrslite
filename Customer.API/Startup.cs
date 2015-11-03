using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Customer.API.Startup))]

namespace Customer.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureCors(app);
            ConfigureAuth(app);
            
        }
    }
}
