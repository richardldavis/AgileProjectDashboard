namespace ProjectDashboard.Models
{
    using System.Collections.Generic;
    using Model.Stories;
using ProjectDashboard.Model.Projects;

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

        public Project Project { get; set; }

        public IList<Story> Stories { get; set; }

        public ViewStoriesOptions ViewOptions { get; set; }

        #endregion
    }
}
