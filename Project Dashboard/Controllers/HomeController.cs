using ProjectDashboard.Domain;
using ProjectDashboard.Models;
using System;
using System.Collections.Generic;
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

            _service = new StoryService(53698, "fda90b3f792f412e9a9b3139f9867ded", fileLoc);
        }

        public ActionResult Index()
        {

            var dashboardModel = new DashboardModel();

            dashboardModel.LatestStories = _service.GetStories().OrderByDescending(x => x.ID).Take(5).ToList();
            
            var list = _service.GetTags()
                      .Select(x => new SelectListItem { Text = x, Value = x })
                      .ToList();

            dashboardModel.Tags = new SelectList(list, "Value", "Text", null); 

            return View(dashboardModel);
           
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


        public ActionResult TagFilter(FormCollection form)
        {
            var tag = form["tags"].ToString();
            return RedirectToAction("Index", new { tagFilter = tag });
        }

        public RedirectToRouteResult SaveActual(FormCollection form)
        {
            var storyID = int.Parse(form["storyID"].ToString());
            var actual = decimal.Parse(form["actual"].ToString());

            _service.SaveActual(storyID, actual);

            return RedirectToAction("Index", "Home");
        }

        public decimal GetEstimateForPriority(int priority)
        {
            return _service.GetTotalEstimateForProject(priority);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
