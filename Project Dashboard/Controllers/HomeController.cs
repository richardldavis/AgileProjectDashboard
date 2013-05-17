using ProjectDashboard.Domain;
using ProjectDashboard.Domain.TimeZoneIntegration;
using ProjectDashboard.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace ProjectDashboard.Controllers
{
    public class HomeController : Controller
    {
        private StoryService _service;
        private TimeZoneService _tzService;

        public HomeController()
        {

            var fileLoc = HostingEnvironment.MapPath("/");

            _service = new StoryService(int.Parse(ConfigurationManager.ConnectionStrings["AgileProjectID"].ConnectionString), ConfigurationManager.ConnectionStrings["AgileKey"].ConnectionString, fileLoc);

            _tzService = new TimeZoneService();

        }

        public ActionResult Index()
        {

            var dashboardModel = new DashboardModel();
           
            var list = _service.GetTags()
                      .Select(x => new SelectListItem { Text = x, Value = x })
                      .ToList();

            dashboardModel.TotalActual = Math.Round(_service.GetStories().Sum(x => x.Actual),2);

            dashboardModel.Tags = new SelectList(list, "Value", "Text", null); 

            return View(dashboardModel);
           
        }

        public ActionResult TakeSnapshot()
        {

            var stories = _service.GetStories();

            var snapshot = new Snapshot { 
                Date = DateTime.Now, 
                TotalNumberOfStories = stories.Count(),  
                StoriesBeingWorkedOn = stories.Where(x => x.Status == "Working").Count(), 
                TotalEstimate = stories.Sum(x => x.Estimate),
                StoriesCompleted = stories.Where(x => x.Status == "Complete").Count(),
                TotalTimeSpent = stories.Sum(x => x.Actual), 
            };

            _service.TakeSnapshot(snapshot);

            return RedirectToAction("Index", new { tagFilter = "" });
        }

        public ActionResult ShowNonStoryTimeZoneEntries()
        {
            return PartialView("_timezoneEntries", _tzService.EntriesNotAssignedToStories());
        }

        public ActionResult ShowEstimates(string tagFilter = "")
        {
            var dashboardModel = new DashboardModel();
            
            dashboardModel.TotalEstimate = _service.GetTotalEstimateForProject(0, tagFilter);
            dashboardModel.EstimateByPriority = new List<KeyValuePair<string, string>>();

            dashboardModel.CompletenessByPriority = new List<Completeness>();
            
            foreach (var p in _service.GetPrioritiesForProject())
            {
                dashboardModel.EstimateByPriority.Add(new KeyValuePair<string, string>(p, _service.GetTotalEstimateForProject(int.Parse(p), tagFilter).ToString()));
            }

            foreach (var p in _service.GetPrioritiesForProject())
            {
                dashboardModel.CompletenessByPriority.Add(new Completeness{ Label = p,  
                                                                            Complete = Math.Round(_service.GetCompletenessForProject("Complete", int.Parse(p), tagFilter), 2), 
                                                                            Working =  Math.Round(_service.GetCompletenessForProject("Working", int.Parse(p), tagFilter), 2) });
            }

            return PartialView("_estimates", dashboardModel);
        }

        public PartialViewResult ShowCurrentWork()
        {
            var dashboardModel = new DashboardModel();
            dashboardModel.StoriesBeingWorkedOn = _service.GetStories().Where(x => x.Status == "Working").ToList();

            return PartialView("_currentWork", dashboardModel);
        }

        public PartialViewResult ShowLatestStories()
        {
            var dashboardModel = new DashboardModel();
            dashboardModel.LatestStories = _service.GetStories().OrderByDescending(x => x.ID).Take(5).ToList();
           
            return PartialView("_latestStories", dashboardModel);
        }

        public PartialViewResult ShowCompletedStories()
        {
            return PartialView("_stories", _service.GetStories().Where(x => x.Status == "Complete").OrderByDescending(x => x.ID).ToList());
        }

        public PartialViewResult ShowLatestComments()
        {
            var dashboardModel = new DashboardModel();
            dashboardModel.LatestComments = _service.GetComments().OrderByDescending(x => x.Date).Take(5).ToList();

            return PartialView("_latestComments", dashboardModel);
        }

        public ActionResult TagFilter(FormCollection form)
        {
            var tag = form["tags"].ToString();
            return RedirectToAction("Index", new { tagFilter = tag });
        }

        //public ContentResult SaveActual(FormCollection form)
        //{
        //    var storyID = int.Parse(form["storyID"].ToString());
        //    var actual = decimal.Parse(form["actual"].ToString());

        //    var c = new ContentResult();

        //    c.Content = Math.Round(_service.SaveActual(storyID, actual) / 7, 2).ToString();
            
        //    return c;
        //}

        public ContentResult CompletedStoryAverageEstimateAccuracy()
        {
            var c = new ContentResult();

            c.Content = Math.Round(_service.CompletedStoryAverageEstimateAccuracy(), 2).ToString();

            return c;
        }

        public ContentResult TimeSpentOnOtherStuff()
        {
            var c = new ContentResult();

            var time = _tzService.EntriesNotAssignedToStories().Where(x => x.Role == "Senior Front End Developer" | x.Role == "Senior Developer" | x.Role == "Developer").Sum(x => x.Time) / 7;

            var overhead = Math.Round(time, 2).ToString() + " which is a " + (Math.Round((time / _service.GetStories().Sum(x => x.Actual)) * 100)).ToString() + "% overhead";

            c.Content = overhead;

            return c;
        }

        public decimal GetEstimateForPriority(int priority)
        {
            return _service.GetTotalEstimateForProject(priority);
        }
    }
}
