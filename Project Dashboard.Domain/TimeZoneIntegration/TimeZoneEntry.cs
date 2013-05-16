using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDashboard.Domain.TimeZoneIntegration
{
    public class TimeZoneEntry
    {
        public DateTime Date { get; set; }
        public decimal Time { get; set; }
        public string Role { get; set; }
        public string User { get; set; }
        public string Description { get; set; }
    }
}
