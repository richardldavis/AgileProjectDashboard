namespace ProjectDashboard.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Caching;

    public class AgileZenCommentRepository : AgileZenBase, ICommentRepository
    {
    
        public AgileZenCommentRepository(int projectID, string apiKey) : base(projectID, apiKey)
        {
        }

        public void Add(int storyID, string text)
        {

            var request = "https://agilezen.com/api/v1/projects/" + _projectID.ToString() + "/stories/" + storyID + "/comments/?apikey=" + _apiKey;

            string postData = "{text: '" + text + "'}";

            Post(request, postData);
        }
    }
}
