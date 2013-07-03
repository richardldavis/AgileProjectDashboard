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
            var request = string.Format("{0}projects/{1}/stories/{2}/comments/?apikey={3}", ApiRootUrl, ProjectId, storyId, ApiKey);
            var postData = string.Format("{{text: '{0}'}}", text);
            Post(request, postData);
        }

        #endregion
    }
}
