using ProjectDashboard.Domain;
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

        public HomeController()
        {

            var fileLoc = HostingEnvironment.MapPath("/");

            _service = new StoryService(int.Parse(ConfigurationManager.ConnectionStrings["AgileProjectID"].ConnectionString), ConfigurationManager.ConnectionStrings["AgileKey"].ConnectionString, fileLoc);
        }

        public ActionResult Index()
        {

            var dashboardModel = new DashboardModel();
           
            var list = _service.GetTags()
                      .Select(x => new SelectListItem { Text = x, Value = x })
                      .ToList();

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
                StoriesCompleted = stories.Where(x => x.Status == "Completed").Count(),
                TotalTimeSpent = stories.Sum(x => x.Actual), 
            };

            _service.TakeSnapshot(snapshot);

            return RedirectToAction("Index", new { tagFilter = "" });
        }

        public ActionResult ShowEstimates(string tagFilter = "")
        {
            var dashboardModel = new DashboardModel();
            
            dashboardModel.TotalEstimate = _service.GetTotalEstimateForProject(0, tagFilter);
            dashboardModel.EstimateByPriority = new List<KeyValuePair<string, string>>();
            
            foreach (var p in _service.GetPrioritiesForProject())
            {
                dashboardModel.EstimateByPriority.Add(new KeyValuePair<string, string>(p, _service.GetTotalEstimateForProject(int.Parse(p), tagFilter).ToString()));
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

        public ContentResult SaveActual(FormCollection form)
        {
            var storyID = int.Parse(form["storyID"].ToString());
            var actual = decimal.Parse(form["actual"].ToString());

            var c = new ContentResult();

            c.Content = Math.Round(_service.SaveActual(storyID, actual) / 7, 2).ToString();
            
            return c;
        }

        public decimal GetEstimateForPriority(int priority)
        {
            return _service.GetTotalEstimateForProject(priority);
        }
    }
}
