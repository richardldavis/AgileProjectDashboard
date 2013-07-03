namespace ProjectDashboard.Model.Stories
{
    using System;
    using System.Collections.Generic;

    public class Story
    {
        public int ID { get; set; }

        public decimal Estimate { get; set; }

        public decimal Actual { get; set; }

        public DateTime TimeLastUpdated { get; set; }
        
        public string Text { get; set; }

        public int Priority { get; set; }

        public string Owner { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Link { get; set; }

        public List<string> Tags { get; set; }

        public string Status { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
