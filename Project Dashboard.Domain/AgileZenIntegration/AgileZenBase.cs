namespace ProjectDashboard.Domain
{
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Xml;

    public class AgileZenBase
    {
        #region Fields

        protected const string ApiRootUrl = "https://agilezen.com/api/v1/";

        #endregion

        #region Constructor

        public AgileZenBase(int projectId, string apiKey)
        {
            ProjectId = projectId;
            ApiKey = apiKey;
        }

        #endregion

        #region Properties

        protected int ProjectId { get; private set; }

        protected string ApiKey { get; private set; }

        #endregion

        #region Methods

        protected static HttpWebResponse Post(string requestString, string postData)
        {
            var addRequest = (HttpWebRequest)WebRequest.Create(requestString);
            addRequest.ContentType = "application/x-www-form-urlencoded";
                    
            addRequest.Method = "POST";

            var encoding = new ASCIIEncoding();
            var data = encoding.GetBytes(postData);
            addRequest.ContentLength = data.Length;

            using (var stream = addRequest.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            return (HttpWebResponse)addRequest.GetResponse();
        }

        protected static XmlDocument GetAgileResponseAsXml(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            var myResponse = request.GetResponse();

            var stream = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);

            var doc = new XmlDocument();
            doc.LoadXml(stream.ReadToEnd());
            return doc;
        }

        #endregion
    }
}
