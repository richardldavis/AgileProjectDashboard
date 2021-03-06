﻿namespace ProjectDashboard.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Web.Hosting;
    using System.Web.Mvc;
    using Domain;
    using Domain.TimeZoneIntegration;
    using Models;
    using ProjectDashboard.Helpers;
    using Zone.Library.Mvc.ActionResults;
    using Zone.Library.Mvc.ActionResults.Pdf;

    public class HomeController : Controller
    {
        #region Fields

        private readonly StoryService _service;
        
        private readonly TimeZoneService _tzService;
        
        private readonly decimal _timeNotAssignedToStories;

        private readonly decimal _overhead;

        #endregion

        #region Constructor

        public HomeController()
        {

            var fileLoc = HostingEnvironment.MapPath("/");

            _service = new StoryService(int.Parse(ConfigurationManager.ConnectionStrings["AgileProjectID"].ConnectionString), ConfigurationManager.ConnectionStrings["AgileKey"].ConnectionString, fileLoc);

            _tzService = new TimeZoneService();

            _timeNotAssignedToStories = Math.Round(_tzService.EntriesNotAssignedToStories().Where(x => x.Role == "Senior Front End Developer" | x.Role == "Senior Developer" | x.Role == "Developer").Sum(x => x.Time) / 7,2);

            _overhead = Math.Round(_timeNotAssignedToStories / _service.GetStories().Sum(x => x.Actual) * 100, 2);
        }

        #endregion

        #region Action methods

        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult Future()
        {
            var dashboardModel = new DashboardModel();

            var list = _service.GetTags()
                     .Select(x => new SelectListItem { Text = x, Value = x })
                     .ToList();
            
            dashboardModel.Tags = new SelectList(list, "Value", "Text", null); 

            return View(dashboardModel);
        }

        public ActionResult Index()
        {
            var dashboardModel = new DashboardModel();
           
            var list = _service.GetTags()
                      .Select(x => new SelectListItem { Text = x, Value = x })
                      .ToList();

            dashboardModel.TotalActual = Math.Round(_service.GetStories().Sum(x => x.Actual),2);

            dashboardModel.OtherStuffOverheadPercentage = _overhead;

            dashboardModel.Phase1DaysOutstanding = _service.GetStories().Where(x => x.Priority == 1 && x.Status == "Ready To Work on").Sum(y => y.Estimate) +
                                                   (_service.GetStories().Where(x => x.Priority == 1 && x.Status == "Working").Sum(y => y.Estimate) * (decimal)0.5);

            dashboardModel.TotalCompleteEstimateValue = Math.Round(_service.GetStories().Where(y => y.Status=="Complete").Sum(x => x.Estimate), 2);

            dashboardModel.TotalCompleteActualValue = Math.Round(_service.GetStories().Where(y => y.Status == "Complete").Sum(x => x.Actual), 2);

            return View(dashboardModel);
           
        }

        public ActionResult TakeSnapshot()
        {

            var stories = _service.GetStories();

            var snapshot = new Snapshot { 
                Date = DateTime.Now.Date, 
                TotalNumberOfStories = stories.Count(),  
                StoriesBeingWorkedOn = stories.Where(x => x.Status == "Working").Count(), 
                TotalEstimate = stories.Sum(x => x.Estimate),
                StoriesCompleted = stories.Where(x => x.Status == "Complete").Count(),
                TotalTimeSpent = stories.Sum(x => x.Actual),
                StoriesCompletedEstimateValue = stories.Where(x => x.Status == "Complete").Sum(x => x.Estimate),
                StoriesCompletedActualValue = stories.Where(x => x.Status == "Complete").Sum(x => x.Actual)
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

            var overhead = "<strong>" + _timeNotAssignedToStories.ToString() + "</strong> days (which is a " + _overhead.ToString() + "% overhead)";

            c.Content = overhead;

            return c;
        }

        public ActionResult Stories(string layout, ViewStoriesOptions viewOptions)
        {
            var project = _service.GetProject();
            var stories = _service.GetStories();
            switch (layout)
            {
                case "minimal":
                    return View("MinimalStories", new StoriesModel
                                                      {
                                                          Project = project,
                                                          Stories = stories,
                                                          ViewOptions = viewOptions,
                                                      });
                case "pdf":
                    var html = NetHelper.RequestUrl(string.Format("{0}://{1}/{2}/{3}?layout=minimal&{4}", Request.Url.Scheme, Request.Url.Authority, ControllerContext.RouteData.Values["controller"], ControllerContext.RouteData.Values["action"], GetQueryString(viewOptions)));
                    return new PdfResult
                               {
                                   HtmlContent = html,
                                   FileName = "stories.pdf",
                               };
                default:
                    return View(new StoriesModel
                                    {
                                        Project = project,
                                        Stories = stories,
                                        ViewOptions = viewOptions,
                                    });
            }
        }

        #endregion

        #region Partial methods

        public PartialViewResult ShowCurrentWork()
        {
            var dashboardModel = new DashboardModel();
            dashboardModel.StoriesBeingWorkedOn = _service.GetStories().Where(x => x.Status == "Working").ToList();

            return PartialView("_currentWork", dashboardModel);
        }

        public PartialViewResult ShowLatestStories()
        {
            var dashboardModel = new DashboardModel();
            dashboardModel.LatestStories = _service.GetStories().OrderByDescending(x => x.Id).Take(5).ToList();
           
            return PartialView("_latestStories", dashboardModel);
        }

        public PartialViewResult ShowCompletedStories()
        {
            return PartialView("_stories", _service.GetStories().Where(x => x.Status == "Complete").OrderByDescending(x => x.Id).ToList());
        }

        public PartialViewResult ShowLatestComments()
        {
            var dashboardModel = new DashboardModel();
            dashboardModel.LatestComments = _service.GetComments().OrderByDescending(x => x.Date).Take(5).ToList();

            return PartialView("_latestComments", dashboardModel);
        }

        #endregion

        #region Helpers

        private static string GetQueryString(object model)
        {
            if (model == null)
            {
                return string.Empty;
            }

            return string.Join("&", model.GetType().GetProperties().Select(p => string.Format("{0}={1}", p.Name, p.GetValue(model) ?? string.Empty)));
        }

        #endregion
    }
}
