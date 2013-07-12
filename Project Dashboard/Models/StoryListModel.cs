namespace ProjectDashboard.Models
{
    using System.Collections.Generic;
    using Model.Stories;

    public class StoryListModel
    {
        #region Constructor

        public StoryListModel()
        {
            ViewOptions = new ViewStoriesOptions();
        }

        #endregion

        public IList<Story> Stories { get; set; }

        public int RootHeaderLevel { get; set; }

        public ViewStoriesOptions ViewOptions { get; set; }
    }
}
