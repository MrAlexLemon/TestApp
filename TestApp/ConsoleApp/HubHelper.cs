using Common.Interfaces;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public static class HubHelper
    {
        public static string HubUrl => "http://localhost:52032/hubs/service";
        public static class Events
        {
            public static string MessageEvent => nameof(IMessage.ShowMessage);
            public static string PingEvent => nameof(IMessage.Ping);
        }
    }
}
