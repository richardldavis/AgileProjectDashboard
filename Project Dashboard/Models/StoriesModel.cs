namespace ProjectDashboard.Models
{
    using System.Collections.Generic;
    using Model.Stories;

    public class StoriesModel
    {
        #region Constructor

        public StoriesModel()
        {
            Stories = new List<Story>();
            ViewOptions = new ViewStoriesOptions();
        }

        #endregion

        #region Properties

        public IList<Story> Stories { get; set; }

        public ViewStoriesOptions ViewOptions { get; set; }

        #endregion
    }
}
