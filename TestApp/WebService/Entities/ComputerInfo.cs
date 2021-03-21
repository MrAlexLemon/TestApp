using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Entities
{
    public class ComputerInfo
    {
        public string ComputerName { get; set; }
        public string TimeZone { get; set; }
        public string OsName { get; set; }
        public string DotNetVersion { get; set; }
        public DateTime ConnectedTime { get; set; }
        public DateTime? DisconnectedTime { get; set; }
    }
}
