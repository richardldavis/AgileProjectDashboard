using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDashboard.Domain
{
    public class StoryAnnotationRepository : IStoryAnnotationRepository
    {
        private int _projectID;
        private string _fileRoot;

        public StoryAnnotationRepository(int projectID, string fileRoot)
        {
            _projectID = projectID;
            _fileRoot = fileRoot;
        }

        public void Save(StoryAnnotation storyAnnotation)
        {
            var fileName = _fileRoot + "App_Data\\actual-" + _projectID + "-" + storyAnnotation.StoryID + ".txt";
              
            string[] lines = storyAnnotation.Annotations.Select(kvp => kvp.Key + "=" + kvp.Value).ToArray();
            File.WriteAllLines(fileName, lines);

        }

        public StoryAnnotation Get(int storyID)
        {
            var fileName = _fileRoot + "App_Data\\actual-" + _projectID + "-" + storyID + ".txt";
            
            var annotations = new Dictionary<string,string>();
            
            if (File.Exists(fileName))
            {
                string[] lines = File.ReadAllLines(fileName);
                annotations = lines.Select(l => l.Split('=')).ToDictionary(a => a[0], a => a[1]);
            }

            return new StoryAnnotation { StoryID = storyID, Annotations = annotations };
          
        }
    }
}
