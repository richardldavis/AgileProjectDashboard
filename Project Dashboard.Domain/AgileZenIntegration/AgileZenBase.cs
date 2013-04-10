using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDashboard.Domain
{
    public class AgileZenBase
    {
        protected int _projectID;
        protected string _apiKey;

        public AgileZenBase(int projectID, string apiKey)
        {
            _projectID = projectID;
            _apiKey = apiKey;
        }


        protected HttpWebResponse Post(string requestString, string postData)
        {
            var addRequest = (HttpWebRequest)WebRequest.Create(requestString);
            addRequest.ContentType = "application/x-www-form-urlencoded";
                    
            addRequest.Method = "POST";

            var encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(postData);
            addRequest.ContentLength = data.Length;

            using (Stream stream = addRequest.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            return (HttpWebResponse)addRequest.GetResponse();
        }
    }
}
