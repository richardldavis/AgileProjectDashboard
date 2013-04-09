using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

namespace ProjectDashboard.Domain
{
    public class AgileZenModel : IStoryRepository
    {
        private int _projectID;
        private string _apiKey;
        
        public AgileZenModel(int projectID, string apiKey)
        {
            _projectID = projectID;
            _apiKey = apiKey;
        }

        //public List<string PeopleInProject()
        //{



        //}

        public void SwapTag(string currentTag, string newTag)
        {

            var agile_url = "https://agilezen.com/api/v1/projects/" + _projectID.ToString() + "/tags/" + currentTag + "/stories.xml?apikey=" + _apiKey;
            
            var stories = GetAgileResponseAsXML(agile_url);

            var request = (HttpWebRequest)WebRequest.Create(agile_url);
         

            foreach(XmlNode node in stories)
            {

                //delete the tag from the story
                var deleteRequest = (HttpWebRequest)WebRequest.Create("https://agilezen.com/api/v1/projects/" + _projectID.ToString() + "/tags/" + currentTag + "/stories/" + node.SelectSingleNode("id").InnerText.ToString() + "?apikey=" + _apiKey);

                deleteRequest.Method = "DELETE";

                HttpWebResponse response = (HttpWebResponse)deleteRequest.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    //delete the tag from the story
                    var addRequest = (HttpWebRequest)WebRequest.Create("https://agilezen.com/api/v1/projects/" + _projectID.ToString() + "/tags/" + newTag + "/stories/?apikey=" + _apiKey);
                    addRequest.ContentType = "application/x-www-form-urlencoded";
                    
                    addRequest.Method = "POST";

                    var encoding = new ASCIIEncoding();
                    string postData = "{id:" + node.SelectSingleNode("id").InnerText.ToString() + "}";
                    byte[] data = encoding.GetBytes(postData);

                    addRequest.ContentLength = data.Length;

                    using (Stream stream = addRequest.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }

                    response = (HttpWebResponse)addRequest.GetResponse();
                }

            }


        }

        public List<string> GetTags()
        {

            var agile_url = "https://agilezen.com/api/v1/projects/" + _projectID + "/tags.xml?apiKey=" + _apiKey;

            var request = (HttpWebRequest)WebRequest.Create(agile_url);

            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            var myResponse = request.GetResponse();

            var stream = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);

            var doc = new XmlDocument();
            doc.LoadXml(stream.ReadToEnd());

            var tags = doc.GetElementsByTagName("tag");

            var taglist = new List<string>();

            foreach (XmlNode node in tags)
            {
                taglist.Add(node.SelectSingleNode("name").InnerText);

            }

            return taglist;

        }
        
        
        private XmlNodeList GetAgileResponseAsXML(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
           
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            var myResponse = request.GetResponse();

            var stream = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);

            var doc = new XmlDocument();
            doc.LoadXml(stream.ReadToEnd());

            return doc.GetElementsByTagName("story");

        }

        private XmlNodeList GetStoriesAsXML()
        {

            var agile_url = "https://agilezen.com/api/v1/projects/" + _projectID.ToString() + "/stories.xml?apikey=" + _apiKey;

            agile_url += "&with=everything";

            return GetAgileResponseAsXML(agile_url);
        
        }

        public List<Story> GetStories()
        {
            
            var stories = new List<Story>();
                        
            foreach (XmlNode node in GetStoriesAsXML())
            {

                var tags = new List<string>();
                foreach (XmlNode tag in node.SelectSingleNode("tags").SelectNodes("tag"))
                {
                    tags.Add(tag.SelectSingleNode("name").InnerText);
                }
                
                stories.Add(new Story
                {
                    CreatedDate = DateTime.Parse(node.SelectSingleNode("metrics").SelectSingleNode("createTime").InnerText),
                    Estimate = decimal.Parse(node.SelectSingleNode("size").InnerText.ToString()),
                    Owner = node.SelectSingleNode("owner").SelectSingleNode("name").InnerText.ToString(),
                    Priority = int.Parse(node.SelectSingleNode("priority").InnerText.ToString()),
                    Text = node.SelectSingleNode("text").InnerText.ToString(),
                    Link = "agilezen.com/project/" + _projectID.ToString() + "/story/" + node.SelectSingleNode("id").InnerText.ToString(),
                    ID = int.Parse(node.SelectSingleNode("id").InnerText.ToString()),
                    Tags = tags,
                    Status = node.SelectSingleNode("phase").SelectSingleNode("name").InnerText.ToString()


                });
            }

           return stories;

        }
              

       
    }
}