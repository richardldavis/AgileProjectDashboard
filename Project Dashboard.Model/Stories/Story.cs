namespace ProjectDashboard.Model.Stories
{
    using System;
    using System.Collections.Generic;

    public class Story
    {
        #region Constructor

        public Story()
        {
            Comments = new List<Comment>();
            Tags = new List<string>();
            Tasks = new List<Task>();
        }

        #endregion

        #region Properties

        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime TimeLastUpdated { get; set; }
        
        public string Text { get; set; }

        public string Details { get; set; }

        public int Priority { get; set; }

        public string Status { get; set; }

        public decimal Estimate { get; set; }

        public decimal Actual { get; set; }

        public string Owner { get; set; }

        public string Link { get; set; }

        public List<Comment> Comments { get; private set; }

        public List<string> Tags { get; private set; }

        public List<Task> Tasks { get; private set; }

        #endregion
    }
}
