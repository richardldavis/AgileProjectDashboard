namespace ProjectDashboard.Models
{
    public class ViewStoriesOptions
    {
        #region Constructor

        public ViewStoriesOptions()
        {
            IncludeComments = true;
        }

        #endregion

        public bool IncludeComments { get; set; }
    }
}
