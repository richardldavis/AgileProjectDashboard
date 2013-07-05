namespace ProjectDashboard.Models
{
    using System.Collections.Generic;
    using Model.Stories;

    public class StoryListModel
    {
        public IList<Story> Stories { get; set; }

        public int RootHeaderLevel { get; set; }
    }
}
