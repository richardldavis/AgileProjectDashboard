using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ProjectDashboard.Domain.TimeZoneIntegration
{
    public interface ITimeZoneService
    {
        IList<TimeZoneEntry> GetTimeZoneEntries();
        decimal TotalTimeForStory(int storyId);
    }

    public class TimeZoneService : ITimeZoneService
    {
        public decimal TotalTimeForStory(int storyId)
        {
            return GetTimeZoneEntries()
                    .Where(x => x.Description.Contains("#" + storyId.ToString()))
                    .Sum(x => x.Time);
        }
        
        
        public IList<TimeZoneEntry> GetTimeZoneEntries()
        {
            //var url = "https://timezone.thisiszone.com/Api/Timesheets?ProjectID=2477";

            //var request = (HttpWebRequest)WebRequest.Create(url);

            //request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            //var myResponse = request.GetResponse();

            //var stream = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);

            var jsonString = File.ReadAllText("C:\\json.txt");
            //var jsonString = stream.ReadToEnd();
            

            return JsonConvert.DeserializeObject<IList<TimeZoneEntry>>(jsonString);
        }
    }
}
