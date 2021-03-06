﻿namespace ProjectDashboard.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Model.Projects;
    using Model.Stories;
    using TimeZoneIntegration;

    public class StoryService
    {
        private readonly IProjectRepository _projectRepo;
        private readonly IStoryRepository _storyRepo;
        private readonly IStoryAnnotationRepository _actualRepo;
        private readonly ICommentRepository _commentRepo;
        private readonly SnapshotRepository _snapshotRepo;
        private readonly IStoryCache _cache;
        private readonly ITimeZoneService _timezone;

        private decimal _budget = (decimal)138.5;

        public StoryService(int projectId, string apiKey, string fileRoot)
        {
            _projectRepo = new AgileZenProjectRepository(projectId, apiKey);
            _storyRepo = new AgileZenStoryRepository(projectId, apiKey);
            _actualRepo = new StoryAnnotationRepository(projectId, fileRoot);
            _commentRepo = new AgileZenCommentRepository(projectId, apiKey);
            _cache = new StoryCache();
            _snapshotRepo = new SnapshotRepository(fileRoot);
            _timezone = new TimeZoneService();

        }

        public Project GetProject()
        {
            return _projectRepo.GetProject();
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

        public List<Comment> GetComments()
        {
            return GetStories().SelectMany(x => x.Comments).ToList();
        }

        public List<Story> GetStories()
        {
            var stories = _cache.GetStories() ?? _cache.AddStories(_storyRepo.GetStories());

            var annotatedStories = new List<Story>();
            
            foreach (var story in stories)
            {

                story.Actual = Math.Round(_timezone.TotalTimeForStory(story.Id) / 7, 2);

                // get annotations
                var annotations = _actualRepo.Get(story.Id).Annotations;
               
                if (annotations.ContainsKey("last-changed"))
                {
                    story.TimeLastUpdated = DateTime.Parse(annotations["last-changed"]);
                }

                annotatedStories.Add(story);

            }

            return annotatedStories.ToList();
        }


        public decimal GetTotalEstimateForProject(int priority = 0, string tag = "")
        {

            return GetStories().Where(x => x.Priority == priority || priority == 0).Where(x => x.Tags.Contains(tag) || tag == "").Sum(x => x.Estimate);
            
        }

        public decimal GetCompletenessForProject(string phase, int priority = 0, string tag = "")
        {
            var actual = GetStories().Where(x => x.Priority == priority || priority == 0).Where(x => x.Tags.Contains(tag) || tag == "").Where(x => x.Status == phase).Sum(x => x.Estimate);
            var estimate = GetTotalEstimateForProject(priority, tag);

            return estimate == 0 ? 0 : (actual / estimate) * 100;

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

        public void ReplaceTextInDetails(string oldValue, string newValue)
        {
            _storyRepo.ReplaceTextInDetails(oldValue, newValue);
        }

        public decimal CompletedStoryAverageEstimateAccuracy()
        {
            return GetStories().Where(x => x.Status == "Complete" && x.Estimate > 0).Average(y => ((y.Actual - y.Estimate) / y.Estimate) * 100);
            
        }


        //public decimal SaveActual(int storyID, decimal actual)
        //{
           
        //    decimal newActual = (decimal)0;
            
        //    //get existing actual
        //    var current = _actualRepo.Get(storyID);
            
        //    //update value
        //    if (current.Annotations.ContainsKey("actual"))
        //    {
        //        newActual = decimal.Parse(current.Annotations["actual"]) + actual;
        //        current.Annotations["actual"] =  newActual.ToString();
        //    }
        //    else
        //    {
        //         newActual = actual;
        //         current.Annotations.Add("actual", newActual.ToString());
        //    }


        //    //update last changed
        //    if (current.Annotations.ContainsKey("last-changed"))
        //    {
        //        current.Annotations["last-changed"] = DateTime.Now.ToString();
        //    }
        //    else
        //    {
        //        current.Annotations.Add("last-changed", DateTime.Now.ToString());
        //    }

       
        //    _actualRepo.Save(current);

        //    // Do not make a comment when adding actual time to story - request from shelly 16.4.13
        //    // TODO:  put back in as a configuration switch?
        //    //_commentRepo.Add(storyID, "Added time: " + actual.ToString() + ". Total time spent on this story is now " + newActual.ToString());

        //    return newActual;
        //}

        public List<string> GetTags()
        {
            return _storyRepo.GetTags();
        }

    }
}
