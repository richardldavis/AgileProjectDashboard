using System.ComponentModel.DataAnnotations;
namespace ProjectDashboard.Models
{
    public class ViewStoriesOptions
    {
        #region Constructor

        public ViewStoriesOptions()
        {
            IncludeComments = true;
            IncludeStatus = true;
            IncludeTasks = true;
        }

        #endregion

        #region Properties

        [Display(Name = "Include comments")]
        public bool IncludeComments { get; set; }

        [Display(Name = "Include status")]
        public bool IncludeStatus { get; set; }

        [Display(Name = "Include tasks")]
        public bool IncludeTasks { get; set; }

        #endregion
    }
}
