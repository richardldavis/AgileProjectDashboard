using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDashboard.Domain
{
    public class StoryAnnotation
    {
        public int StoryID { get; set; }

        public Dictionary<string, string> Annotations;
    }
}
