using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDashboard.Domain
{
    public class StoryService
    {
        private IStoryRepository _storyRepo;
        private IStoryAnnotationRepository _actualRepo;
        private readonly IStoryCache _cache;

        public StoryService(int projectID, string apiKey, string fileRoot)
        {
            _storyRepo = new AgileZenModel(projectID, apiKey);
            _actualRepo = new StoryAnnotationRepository(projectID, fileRoot);
            _cache = new StoryCache();
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

        public List<Story> GetStories()
        {
            var stories = _cache.GetStories() == null ?
                          _cache.AddStories(_storyRepo.GetStories()) : // the add method returns the stories
                          _cache.GetStories();
      
            foreach (var story in stories)
            {
                
                // get annotations
                var annotations = _actualRepo.Get(story.ID).Annotations;
                
                if (annotations.ContainsKey("actual"))
                {
                     story.Actual = decimal.Parse(annotations["actual"]);
                }

               
            }

            return stories.ToList();
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
            
            //get existing actual
            var current = _actualRepo.Get(storyID);
            
            //update value
            if (current.Annotations.ContainsKey("actual"))
            {
                actual += decimal.Parse(current.Annotations["actual"]);
                current.Annotations["actual"] = actual.ToString();
            }
            else
            {
                current.Annotations.Add("actual", actual.ToString());
            }
          
            _actualRepo.Save(current);

            return actual;
        }

        public List<string> GetTags()
        {
            return _storyRepo.GetTags();
        }

    }
}
