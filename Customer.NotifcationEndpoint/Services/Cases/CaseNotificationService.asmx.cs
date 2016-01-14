using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Serialization;

namespace Customer.NotifcationEndpoint.Services.Cases
{
    /// <summary>
    /// Summary description for CaseNotificationService
    /// </summary>
    public class CaseNotificationService : System.Web.Services.WebService , INotificationBinding
    {
        public notificationsResponse notifications(notifications notifications)
        {
            var caseNotifications = notifications.Notification;

            foreach(var caseNotification in caseNotifications)
            {
                var @case = caseNotification.sObject;
                var id = caseNotification.Id;
            }

            return new notificationsResponse()
            {
                Ack = true
            };
        }
    }
}
