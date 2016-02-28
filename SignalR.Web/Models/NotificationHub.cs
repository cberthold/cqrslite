using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Infrastructure.Events;

namespace SignalR.Web.Models
{
    public class NotificationHub : Hub
    {
        public void PublishEvents(params Infrastructure.Events.IEvent[] @events)
        {
            Clients.All.hello();
        }
    }
}