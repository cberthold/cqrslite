using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Serialization;

namespace Customer.NotifcationEndpoint.Services.Accounts
{
    /// <summary>
    /// Summary description for AccountNotificationService
    /// </summary>
     public class AccountNotificationService : System.Web.Services.WebService, INotificationBinding
    {
        
        public notificationsResponse notifications(notifications notifications1)
        {
            var accountNotification = notifications1.Notification;
            foreach(var item in accountNotification)
            {
                var account = item.sObject;
                var name = account.Name;
            }

            var response = new notificationsResponse()
            {
                Ack = true
            };

            return response;
        }
    }
}
