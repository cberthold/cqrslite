using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext
{
    public class Constants
    {
        public static readonly Guid SECURITY_API = new Guid(@"4D04D504-71A5-4D2A-8A4E-16327F0D6BD7");
        public static readonly Guid CUSTOMER_API = new Guid(@"09B1B02B-9369-4AAB-A91A-0DFACC51F86F");
        public static readonly Guid SIGNALR_API = new Guid(@"138CC799-0FF5-42C1-A7FA-099ECA359F9E");

        public const string SECURITY_API_NAME = "Security API";
        public const string CUSTOMER_API_NAME = "Customer API";
        public const string SIGNALR_API_NAME = "SignalR API";
        
        public static readonly Guid FEATURE_BOOK = Guid.Parse("DBB318CA-EBBE-4683-B687-4550F32B77D1");
        public static readonly Guid ADMIN_POLICY = Guid.Parse("E1A320E7-36C3-4D85-B4EC-2709EAD982E8");
        public static readonly Guid CUSTOMER_POLICY = Guid.Parse("9B532F10-A3B9-4A63-A87A-038C9D30FD9E");
    }
}
