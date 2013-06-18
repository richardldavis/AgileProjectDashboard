using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDashboard.Domain
{
    public class Snapshot
    {
        public DateTime Date { get; set; }

        public int TotalNumberOfStories { get; set; }

        public int StoriesBeingWorkedOn { get; set; }

        public int StoriesCompleted { get; set; }

        public decimal TotalEstimate { get; set; }

        public decimal TotalTimeSpent { get; set; }

        public decimal StoriesCompletedEstimateValue { get; set; }

        public decimal StoriesCompletedActualValue { get; set; }
    }


    public class SnapshotRepository : BaseTextfileRepository
    {

        public SnapshotRepository(string fileRoot) : base(fileRoot)
        {

        }

        public void Save(Snapshot snapshot)
        {
            var fileName = _fileRoot + "App_Data\\snapshots.txt";

            var snapshotText = snapshot.Date + ","
                             + snapshot.StoriesBeingWorkedOn + ","
                             + snapshot.StoriesCompleted + ","
                             + snapshot.TotalEstimate + ","
                             + snapshot.TotalNumberOfStories + ","
                             + snapshot.TotalTimeSpent + ","
                             + snapshot.StoriesCompletedEstimateValue + ","
                             + snapshot.StoriesCompletedActualValue;

            using (StreamWriter w = File.AppendText(fileName))
            {
                w.WriteLine(snapshotText);
            }

        }

        //public StoryAnnotation Get(int storyID)
        //{
        //    //var fileName = _fileRoot + "App_Data\\actual-" + _projectID + "-" + storyID + ".txt";
            
        //    //var annotations = new Dictionary<string,string>();
            
        //    //if (File.Exists(fileName))
        //    //{
        //    //    string[] lines = File.ReadAllLines(fileName);
        //    //    annotations = lines.Select(l => l.Split('=')).ToDictionary(a => a[0], a => a[1]);
        //    //}

        //    //return new StoryAnnotation { StoryID = storyID, Annotations = annotations };
          
        //}
    }
}
