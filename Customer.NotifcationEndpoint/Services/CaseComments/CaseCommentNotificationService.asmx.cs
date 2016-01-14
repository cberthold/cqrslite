using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Serialization;

namespace Customer.NotifcationEndpoint.Services.CaseComments
{
    /// <summary>
    /// Summary description for CaseCommentNotificationService
    /// </summary>
    public class CaseCommentNotificationService : System.Web.Services.WebService, INotificationBinding
    {
        public notificationsResponse notifications(notifications notifications)
        {
            var caseCommentNotifications = notifications.Notification;

            foreach(var notification in caseCommentNotifications)
            {
                var id = notification.Id;
                var comment = notification.sObject;
            }

            return new notificationsResponse()
            {
                Ack = true
            };
        }
    }
}
