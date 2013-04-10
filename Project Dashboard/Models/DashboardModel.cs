using ProjectDashboard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectDashboard.Models
{
    public class DashboardModel
    {

        public decimal TotalEstimate { get; set; }

        public List<KeyValuePair<string, string>> EstimateByPriority { get; set; }

        // ref to a domain model - consider changing
        public List<Story> LatestStories { get; set; }

        public List<Story> StoriesBeingWorkedOn { get; set; }

        public SelectList Tags { get; set; }


    }
}