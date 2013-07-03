namespace ProjectDashboard.Domain
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Xml;
    using Model.Stories;
    using Helpers;

    public class AgileZenStoryRepository : AgileZenBase, IStoryRepository
    {
        #region Constructor

        public AgileZenStoryRepository(int projectId, string apiKey)
            : base(projectId, apiKey)
        {
        }

        #endregion

        #region Methods

        public void SwapTag(string currentTag, string newTag)
        {
            var url = string.Format("{0}projects/{1}/tags/{2}/stories.xml?apikey={3}", ApiRootUrl, ProjectId, currentTag, ApiKey);
            var stories = GetAgileResponseAsXml(url);
            foreach (XmlNode node in stories)
            {
                //delete the tag from the story
                var deleteRequest = (HttpWebRequest)WebRequest.Create(string.Format("{0}projects/{1}/tags/{2}/stories/{3}?apikey={4}", ApiRootUrl, ProjectId, currentTag, node.SelectSingleNode("id").InnerText, ApiKey));
                deleteRequest.Method = "DELETE";
                var response = (HttpWebResponse)deleteRequest.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var addRequest = (HttpWebRequest)WebRequest.Create(string.Format("{0}projects/{1}/tags/{2}/stories/?apikey={3}", ApiRootUrl, ProjectId, newTag, ApiKey));
                    addRequest.ContentType = "application/x-www-form-urlencoded";
                    addRequest.Method = "POST";
                    var encoding = new ASCIIEncoding();
                    var postData = string.Format("{{id:{0}}}", node.SelectSingleNode("id").InnerText);
                    var data = encoding.GetBytes(postData);
                    addRequest.ContentLength = data.Length;

                    using (var stream = addRequest.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }

                    response = (HttpWebResponse)addRequest.GetResponse();
                }
            }
        }

        public List<string> GetTags()
        {
            var url = string.Format("{0}projects/{1}/tags.xml?apiKey={2}", ApiRootUrl, ProjectId, ApiKey);
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            var myResponse = request.GetResponse();

            var stream = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);

            var doc = new XmlDocument();
            doc.LoadXml(stream.ReadToEnd());

            var tags = doc.GetElementsByTagName("tag");
            return tags.Cast<XmlNode>().Select(i => i.SelectSingleNode("name").InnerText).ToList();
        }

        public List<Story> GetStories()
        {
            var stories = new List<Story>();
            foreach (XmlNode node in GetStoriesAsXml())
            {
                var story = new Story
                                {
                                    Id = int.Parse(node.GetText("id")),
                                    CreatedDate = DateTime.Parse(node.GetText("metrics/createTime")),
                                    Text = node.GetText("text"),
                                    Details = node.GetText("details"),
                                    Priority = string.IsNullOrEmpty(node.GetText("priority")) ? 0 : int.Parse(node.GetText("priority")),
                                    Status = node.GetText("phase/name"),
                                    Estimate = string.IsNullOrEmpty(node.GetText("size")) ? 0 : decimal.Parse(node.GetText("size")),
                                    Owner = node.GetText("owner/name"),
                                    Link = string.Format("agilezen.com/project/{0}/story/{1}", ProjectId, node.GetText("id")),
                                };
                story.Comments.AddRange(node.SelectNodes("comments/comment").Cast<XmlNode>()
                                            .Select(i => new Comment
                                                             {
                                                                 Story = story.Id,
                                                                 Who = i.GetText("author/name"),
                                                                 Date = DateTime.Parse(i.GetText("createTime")),
                                                                 Text = i.GetText("text"),
                                                             }));
                story.Tags.AddRange(node.SelectNodes("tags/tag").Cast<XmlNode>()
                                        .Select(i => i.GetText("name")));
                story.Tasks.AddRange(node.SelectNodes("tasks/task").Cast<XmlNode>()
                                         .Select(i =>
                                                     {
                                                         var task = new Task
                                                                        {
                                                                            Story = story.Id,
                                                                            Text = i.GetText("text"),
                                                                            Complete = "complete".Equals(i.GetText("status")),
                                                                        };
                                                         if (task.Complete)
                                                         {
                                                             task.FinishedDate = DateTime.Parse(i.GetText("finishTime"));
                                                             task.FinishedBy = i.GetText("finishedBy/name");
                                                         }

                                                         return task;
                                                     }));

                stories.Add(story);
            }

            return stories;
        }

        #endregion

        #region Helpers

        private static XmlNodeList GetAgileResponseAsXml(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            var myResponse = request.GetResponse();

            var stream = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);

            var doc = new XmlDocument();
            doc.LoadXml(stream.ReadToEnd());
            return doc.GetElementsByTagName("story");
        }

        private XmlNodeList GetStoriesAsXml()
        {
            var url = string.Format("{0}projects/{1}/stories.xml?apikey={2}", ApiRootUrl, ProjectId, ApiKey)
                      + "&with=everything&pageSize=1000";
            return GetAgileResponseAsXml(url);
        }

        #endregion
    }
}
