using ProjectDashboard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectDashboard.Model.Stories;

namespace ProjectDashboard.Models
{
    public class Completeness
    {
        public string Label { get; set; }

        public decimal Working { get; set; }

        public decimal Complete { get; set; }
    }

    
    public class DashboardModel
    {
        public decimal TotalEstimate { get; set; }

        public decimal TotalActual { get; set; }

        public decimal OtherStuffOverheadPercentage { get; set; }

        public decimal DaysToCompletion
        {
            get { return Phase1DaysOutstanding == 0 ? 0 : Math.Round((OtherStuffOverheadPercentage / 100 * Phase1DaysOutstanding) + (Phase1DaysOutstanding * EstimateAccuracy / 100), 2); }
        }

        public decimal Phase1DaysOutstanding { get; set; }

        public decimal EstimateAccuracy
        {
            get { return TotalCompleteEstimateValue == 0 ? 0 : Math.Round(100 - ((TotalCompleteEstimateValue - TotalCompleteActualValue) / TotalCompleteEstimateValue) * 100, 2); }
        }

        public decimal TotalCompleteEstimateValue { get; set; }

        public decimal TotalCompleteActualValue { get; set; }  

        public List<KeyValuePair<string, string>> EstimateByPriority { get; set; }

        public List<Completeness> CompletenessByPriority { get; set; }

        // ref to a domain model - consider changing
        public List<Story> LatestStories { get; set; }

        public List<Story> StoriesBeingWorkedOn { get; set; }

        public SelectList Tags { get; set; }

        public List<Comment> LatestComments { get; set; }
    }
}