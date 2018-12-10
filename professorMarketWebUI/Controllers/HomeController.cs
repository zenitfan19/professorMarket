using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace professorMarketWebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.TutorCount = BLL.Data.AdditionalData.GetTutorsCount();
            ViewBag.SubjectCount = BLL.Data.AdditionalData.GetSubjectCount();
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}