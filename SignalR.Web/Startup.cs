using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SignalR.Web.Startup))]

namespace SignalR.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureSignalR(app);
            ConfigureAuth(app);
        }
    }
}
