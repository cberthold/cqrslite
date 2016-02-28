using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using SignalR.Web.Providers;
using SignalR.Web.Models;

namespace SignalR.Web
{
    public partial class Startup
    {
        public void ConfigureSignalR(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
