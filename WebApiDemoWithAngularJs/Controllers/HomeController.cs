using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApiDemoWithAngularJs.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult FormValidation()
        {
            ViewBag.Title = "Form Validation example";
            return View();
        }

    }
}
