namespace ProjectDashboard.Domain
{
    public class AgileZenCommentRepository : AgileZenBase, ICommentRepository
    {
        #region Constructor

        public AgileZenCommentRepository(int projectId, string apiKey)
            : base(projectId, apiKey)
        {
        }

        #endregion

        #region Methods

        public void Add(int storyId, string text)
        {
            var request = string.Format("https://agilezen.com/api/v1/projects/{0}/stories/{1}/comments/?apikey={2}", ProjectId, storyId, ApiKey);
            var postData = string.Format("{{text: '{0}'}}", text);
            Post(request, postData);
        }

        #endregion
    }
}
