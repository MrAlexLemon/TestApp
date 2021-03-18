using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class ComputerInfo
    {
        public string ComputerName { get; set; }
        public string TimeZone { get; set; }
        public string OsName { get; set; }
        public string DotNetVersion { get; set; }
        public ComputerStatus ComputerStatus { get; set; }
    }
}
