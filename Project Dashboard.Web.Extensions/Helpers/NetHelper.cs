namespace ProjectDashboard.Helpers
{
    using System.IO;
    using System.Net;

    public static class NetHelper
    {
        public static string RequestUrl(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();
            var responseStream = response.GetResponseStream();
            string content = null;
            if (responseStream != null)
            {
                var reader = new StreamReader(responseStream);
                content = reader.ReadToEnd();
                reader.Close();
            }

            return content;
        }
    }
}
