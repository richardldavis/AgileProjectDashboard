using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDashboard.Domain
{
    public class StoryService
    {
        private readonly IStoryRepository _storyRepo;
        private readonly IStoryAnnotationRepository _actualRepo;
        private readonly ICommentRepository _commentRepo;
        private readonly SnapshotRepository _snapshotRepo;
        private readonly IStoryCache _cache;

        public StoryService(int projectID, string apiKey, string fileRoot)
        {
            _storyRepo = new AgileZenModel(projectID, apiKey);
            _actualRepo = new StoryAnnotationRepository(projectID, fileRoot);
            _commentRepo = new AgileZenCommentRepository(projectID, apiKey);
            _cache = new StoryCache();
            _snapshotRepo = new SnapshotRepository(fileRoot);
        }

        public List<string> GetPrioritiesForProject()
        { 
            var priorities = new HashSet<string>();
            
            foreach (Story story in  GetStories())
            {
                priorities.Add(story.Priority.ToString());
            }

            return priorities.ToList();
        
        }

        public void TakeSnapshot(Snapshot snapshot)
        {
            _snapshotRepo.Save(snapshot);
        }

        public List<Story> GetStories()
        {
            var stories = _cache.GetStories() == null ?
                          _cache.AddStories(_storyRepo.GetStories()) : // the add method returns the stories
                          _cache.GetStories();

            var annotatedStories = new List<Story>();
            
            foreach (var story in stories)
            {
                
                // get annotations
                var annotations = _actualRepo.Get(story.ID).Annotations;
                
                if (annotations.ContainsKey("actual"))
                {
                     story.Actual = decimal.Parse(annotations["actual"]) / 7;
                }

                annotatedStories.Add(story);

            }

            return annotatedStories.ToList();
        }


        public decimal GetTotalEstimateForProject(int priority = 0, string tag = "")
        {

            return GetStories().Where(x => x.Priority == priority || priority == 0).Where(x => x.Tags.Contains(tag) || tag == "").Sum(x => x.Estimate);
            
        }

        public decimal GetTotalEstimateForStories(List<Story> stories)
        {
            decimal estimateCount = 0;

            foreach (Story story in stories)
            {
                estimateCount += story.Estimate;
            }

            return estimateCount;
        }


        public void SwapTag(string currentTag, string newTag)
        {
            _storyRepo.SwapTag(currentTag, newTag);
        }


        public decimal SaveActual(int storyID, decimal actual)
        {
           
            decimal newActual = (decimal)0;
            
            //get existing actual
            var current = _actualRepo.Get(storyID);
            
            //update value
            if (current.Annotations.ContainsKey("actual"))
            {
                newActual = decimal.Parse(current.Annotations["actual"]) + actual;
                current.Annotations["actual"] =  newActual.ToString();
            }
            else
            {
                 newActual = actual;
                 current.Annotations.Add("actual", newActual.ToString());
            }
       
            _actualRepo.Save(current);

            _commentRepo.Add(storyID, "Added time: " + actual.ToString() + ". Total time spent on this story is now " + newActual.ToString());

            return newActual;
        }

        public List<string> GetTags()
        {
            return _storyRepo.GetTags();
        }

    }
}
