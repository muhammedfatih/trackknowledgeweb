using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackKnowledgeWeb.Helpers;

namespace TrackKnowledgeWeb.Controllers
{
    [AFAuthorization]
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}